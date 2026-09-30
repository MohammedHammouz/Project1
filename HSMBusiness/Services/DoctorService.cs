using Azure;
using HSMBusiness.dto;
using HSMBusiness.Pattern;
using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness
{
    public class DoctorService
    {
        ResultPatern resultPattern = new ResultPatern();
        public enum enMode { Add = 0, Update }
        public enMode mode = enMode.Add;
        private readonly DoctorRepository _doctor;
        private readonly UserService _userService;
        private readonly DepartmentService _departmentService;
        public DoctorRepository doctorRepository { get { return _doctor; } }
        private readonly UserRepository _userRepository;
        public DoctorService(DepartmentService departmentService, DoctorRepository doctor,UserService userService,UserRepository userRepository, enMode mode = enMode.Add)
        {
            _doctor = doctor;
            _departmentService = departmentService;
            _userService = userService;
            _userRepository = userRepository;
            this.mode = mode;
        }
        
        public async Task<(int Status,string? Response,bool IsSuccess,DoctorDTO? doctorDTO1, Dictionary<string, string>? errors)> GetDoctorByID(string DoctorID)
        {
            Doctor doctor = await _doctor.GetByIDAsync(DoctorID);
            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (doctor == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>(),"Doctor");
                return (response.Status,response.Response,response.IsSuccess,null,response.Errors);
            }
            return (response.Status,response.Response, response.IsSuccess, new DoctorDTO(DoctorID, doctor.DepartmentID, doctor.Specialization, doctor.Status,
                 doctor.UserID), response.Errors);
        }
        public async Task<(int Status,string?Response,bool IsSuccess, Dictionary<string, string>? errors)> Delete(string DoctorID)
        {
            Doctor doctor = await _doctor.GetByIDAsync(DoctorID);
            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (doctor == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, response.Errors);
            }
            bool IsDeleted = await _doctor.DeleteAsync(doctor);
            if (!IsDeleted)
            {
                var error =await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, response.Errors);
            }
            return (response.Status, response.Response, response.IsSuccess, response.Errors);
        }
        public async Task<(int Status,string?Response,bool IsSuccess,List<DoctorDTO>?doctorsList)> GetAll()
        {
            var doctors = await _doctor.GetAllAsync();
            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (doctors == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess,null);
            }

            return (response.Status,response.Response,response.IsSuccess, doctors.Select(d => new DoctorMapper().ToDTO(d))
        .ToList());
        }
        private async Task<bool> Add(DoctorDTO doctorDTO)
        {
            
            var doctorEntity = new DoctorMapper().ToEntity(doctorDTO);
            if (!await _departmentService.IsDepartmentExist(doctorEntity, doctorDTO)  && !await  _userService.IsUserExist(doctorEntity, doctorDTO))
            {
                return false;
            }
            doctorEntity.User = await _userRepository.GetByIDAsync(doctorEntity.UserID);
            if (doctorEntity.User == null)
            {
                return false;
            }
            if(doctorEntity.User.Role == "Doctor")
            {
                var AddNew = await _doctor.AddAsync(doctorEntity);
                doctorEntity.ID = AddNew.ID;
            }
            
            return doctorEntity.ID != "";
        }
        public async Task<bool> IsDoctorExist(Appointment appointmentEntity, AppointmentDTO appointmentDTO)
        {

            var doctor = await GetDoctorByID(appointmentEntity.DoctorID);
            if (doctor.doctorDTO1 == null)
            {
                return false;
            }

            return true;
        }
        private async Task<(int, string?, bool, Dictionary<string, string>? errors)> Update(string ID, DoctorDTO doctorDTO)
        {
            Doctor? doctorEntity = await _doctor.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (doctorEntity == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, response.Errors);
            }
            if( !await _departmentService.IsDepartmentExist(doctorEntity,doctorDTO)  && await _userService.IsUserExist(doctorEntity, doctorDTO))
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, response.Errors);
            }
            doctorEntity.User = await _userRepository.GetByIDAsync(doctorEntity.UserID);
            if (doctorEntity.User == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, response.Errors);
            }
            if (doctorEntity.User.Role == "Doctor")
            {
                var AddNew = await _doctor.AddAsync(doctorEntity);
                doctorEntity.ID = AddNew.ID;
            }
            doctorEntity = new DoctorMapper().ToEntity(doctorDTO,DoctorMapper.enMode.Update,doctorEntity);
            await _doctor.UpdateAsync(doctorEntity);
            return (response.Status, response.Response, response.IsSuccess, response.Errors);
        }
        public async Task<(int Status, string?Response, bool IsSuccess, Dictionary<string, string>? errors)> Save(DoctorDTO doctorDTO,string ID)
        {
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            switch (mode)
            {
                case enMode.Add:
                    
                    if (await Add(doctorDTO))
                    {
                        mode = enMode.Update;
                    }
                    else
                    {
                        response =await  resultPattern.GiveResponse(400, new Dictionary<string, string>());
                        
                    }
                    return (response.Status, response.Response, response.IsSuccess, response.Errors);
                case enMode.Update:
                    return await Update(ID, doctorDTO);
            }
            response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
            return (response.Status, response.Response, response.IsSuccess, response.Errors);
        }
    }
}
