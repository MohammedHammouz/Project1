using Azure;
using HSMBusiness.dto;
using HSMBusiness.Pattern;
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
        public DoctorRepository doctorRepository { get { return _doctor; } }
        public DoctorService( DoctorRepository doctor, enMode mode = enMode.Add)
        {
            _doctor = doctor;
            this.mode = mode;
        }
        
        public async Task<(string?,bool,DoctorDTO)> GetDoctorByID(string DoctorID)
        {
            Doctor doctor = await _doctor.GetByIDAsync(DoctorID);
            var response =await resultPattern.GiveResponse(200);
            if (doctor == null)
            {
                response =await resultPattern.GiveResponse(404);
                return (response.Response,response.IsSuccess,null);
            }
            return (response.Response, response.IsSuccess, new DoctorDTO(DoctorID, doctor.DepartmentID, doctor.Specialization, doctor.Status,
                 doctor.UserID));
        }
        public async Task<(string?,bool)> Delete(string DoctorID)
        {
            Doctor doctor = await _doctor.GetByIDAsync(DoctorID);
            var response =await resultPattern.GiveResponse(200);
            if (doctor == null)
            {
                response =await resultPattern.GiveResponse(404);
                return (response.Response,response.IsSuccess);
            }
            bool IsDeleted = await _doctor.DeleteAsync(doctor);
            if (!IsDeleted)
            {
                var error =await resultPattern.GiveResponse(500);
                return (error.Response, error.IsSuccess);
            }
            return (response.Response,response.IsSuccess);
        }
        public async Task<(string?,bool,List<DoctorDTO>)> GetAll()
        {
            var doctors = await _doctor.GetAllAsync();
            var response =await resultPattern.GiveResponse(200);
            if (doctors == null)
            {
                response =await resultPattern.GiveResponse(500);
                return (response.Response,response.IsSuccess,null);
            }

            return (response.Response,response.IsSuccess, doctors.Select(d => new DoctorDTO(d.ID,d.DepartmentID,d.Specialization,d.Status,d.UserID))
        .ToList());
        }
        private async Task<bool> Add(DoctorDTO doctorDTO)
        {
            var doctorEntity = new DoctorMapper().ToEntity(doctorDTO);
            var AddNew = await _doctor.AddAsync(doctorEntity);
            doctorEntity.ID = AddNew.ID;
            return doctorEntity.ID != "";
        }
        private async Task<(string?, bool)> Update(string ID)
        {
            Doctor? doctorEntity = await _doctor.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200);
            if (doctorEntity == null)
            {
                response = await resultPattern.GiveResponse(404);
                return (response.Response,response.IsSuccess);
            }
            response =await resultPattern.GiveResponse(200);
            return (response.Response,await _doctor.UpdateAsync(doctorEntity));
        }
        public async Task<(string?,bool)> Save(DoctorDTO doctorDTO=null,string ID="")
        {
            var response = await resultPattern.GiveResponse(200);
            switch (mode)
            {
                case enMode.Add:
                    
                    if (await Add(doctorDTO))
                    {
                        mode = enMode.Update;
                        return (response.Response, response.IsSuccess);
                    }
                    else
                    {
                        response =await  resultPattern.GiveResponse(500);
                        return  (response.Response,response.IsSuccess);
                    }
                case enMode.Update:
                    return await Update(ID);
            }
            response = await resultPattern.GiveResponse(500);
            return (response.Response,response.IsSuccess);
        }
    }
}
