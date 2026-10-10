using HSMBusiness.Erorrs;
using HSMBusiness.Mappers;
using HSMBusiness.Pattern;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HSMBusiness.Services
{
    public class AppointmentService
    {
        ResultPatern resultPattern = new ResultPatern();
        public enum enMode { Add = 0, Update }
        public enMode _mode = enMode.Add;
        private readonly AppointmentRepository _appointment;
        private readonly AppointmentError appointmentError;
        private readonly PatientService patientService;
        private readonly DoctorService doctorService;
        public AppointmentRepository appointmentRepository { get { return _appointment; } }
        public AppointmentService(AppointmentRepository appointment,AppointmentError appointmentError,PatientService patientService,DoctorService doctorService,enMode mode=enMode.Add)
        {
            _appointment = appointment;
            this.appointmentError = appointmentError;
            this.patientService = patientService;
            this.doctorService = doctorService;
            _mode = mode;
        }
        private async Task<(int Status,bool IsSuccess)> Add(AppointmentDTO appointmentDTO)
        {
            var appointmentEntity = await new AppointmentMapper().ToEntity(appointmentDTO);
            if (appointmentEntity == null)
            {
                return (404,false);
            }
            if(appointmentDTO.Status=="Reschedualed" || appointmentDTO.Status == "Cancelled")
            {
                return (500, false);
            }
            if (!await doctorService.IsDoctorExist(appointmentEntity, appointmentDTO) && !await patientService.IsPatientExist(appointmentEntity, appointmentDTO))
            {
                return (500,false);
            }
            if(await appointmentRepository.DoctorHasAppointment(appointmentEntity.DoctorID,appointmentEntity.Date,appointmentEntity.Time))
            {
                return (500, false);
            }
            if (await appointmentRepository.PatientHasAppointment(appointmentEntity.PatientID, appointmentEntity.Date, appointmentEntity.Time))
            {
                return (500, false);
            }
            if (appointmentEntity.Status != "Scheduled")
            {
                return (500, false);
            }
            var AddNew = await _appointment.AddAsync(appointmentEntity);
            appointmentEntity.ID = AddNew.ID;
            if(appointmentEntity.ID == "")
            {
                return (500, false);
            }
            return  (200, false);
        }

        private async Task<bool>CheckUpdatedTimeEqualesCurrentTime(DateOnly CurrntDate,TimeOnly? CurrentTime, DateOnly UpdateDate, TimeOnly? UpdateTime)
        {
            return (CurrntDate == UpdateDate && CurrentTime == UpdateTime);
        }
        private async Task<(int,string?,bool, Dictionary<string, string>? errors)>Update(string ID,AppointmentDTO appointmentDTO)
        {
            var CurrentAppointment = await _appointment.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200,new Dictionary<string,string>());
            
            if (appointmentDTO.Status == "Scheduled")
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, response.Errors);
            }
            if(await CheckUpdatedTimeEqualesCurrentTime(CurrentAppointment.Date, CurrentAppointment.Time, appointmentDTO.Date, appointmentDTO.Time))
            {
                response = await resultPattern.GiveResponse(400, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, response.Errors);
            }
            if (CurrentAppointment == null)
            {
                response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess,response.Errors);
            }
            if(await _appointment.CountCancelledAppointment(appointmentDTO.PatientID, appointmentDTO.DoctorID, appointmentDTO.Date, appointmentDTO.Time, appointmentDTO.Status))
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, response.Errors);
            }
            if(!await doctorService.IsDoctorExist(CurrentAppointment, appointmentDTO) && !await patientService.IsPatientExist(CurrentAppointment, appointmentDTO) )
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, response.Errors);
            }
            if (ID == CurrentAppointment.ID)
            {

            }
            if (await appointmentRepository.DoctorHasAppointment(CurrentAppointment.DoctorID, CurrentAppointment.Date, CurrentAppointment.Time,CurrentAppointment.ID) && appointmentDTO.Status != "Cancelled")
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, response.Errors);
            }
            if (await appointmentRepository.PatientHasAppointment(CurrentAppointment.PatientID, CurrentAppointment.Date, CurrentAppointment.Time, CurrentAppointment.ID) && appointmentDTO.Status != "Cancelled")
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, response.Errors);
            }
            var errors = await appointmentError.ValidateAppointment(appointmentDTO);
            
            if (errors.Count > 0)
            {
                response =
                await resultPattern.GiveResponse(500, errors);
                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess,
                    response.Errors
                );
            }
            if (CurrentAppointment.Status == "Cancelled")
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, response.Errors);
            }
            CurrentAppointment = await new AppointmentMapper().ToEntity(appointmentDTO,AppointmentMapper.enMode.Update,CurrentAppointment);

            await _appointment.UpdateAsync(CurrentAppointment);
            
            return (response.Status, response.Response, response.IsSuccess, response.Errors);
        }
        public async Task<(int Status, string? Response, bool IsSuccess, Dictionary<string, string>? errors)>Save(AppointmentDTO appointmentDTO,string ID)
        {
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            var errors = await appointmentError.ValidateAppointment(appointmentDTO);

            if (errors.Count > 0)
            {
                response =
                await resultPattern.GiveResponse(500, errors);
                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess,
                    response.Errors
                );
            }
            switch (_mode)
            {
                 case enMode.Add:
                    var AddNew = await Add(appointmentDTO);
                    if (AddNew.IsSuccess)
                    {
                        _mode = enMode.Update;
                        
                    }
                    else
                    {
                        response = await resultPattern.GiveResponse(AddNew.Status, new Dictionary<string, string>());
                    }
                    return (response.Status, response.Response, response.IsSuccess, response.Errors);
                 case enMode.Update:
                    return await Update(ID, appointmentDTO);
            }
            response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
            return (response.Status, response.Response, response.IsSuccess, response.Errors);
        }
        public async Task<(int Status,string? Response, bool IsSuccess, List<AppointmentDTO>?appointmentList, Dictionary<string, string>? errors)> GetAll()
        {
            var appointments = await _appointment.GetAllAsync();
            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (appointments == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status,response.Response, response.IsSuccess, null,response.Errors);
            }
            return (response.Status, response.Response, response.IsSuccess, appointments.Select(a=> new AppointmentDTO(a.ID,a.PatientID,a.DoctorID,a.Date,a.Time,a.Duration,a.Status,a.NotificationSent)).ToList(),response.Errors);
        }
        public async Task<(int Status,string?Response, bool IsSuccess, AppointmentDTO? appointmentDTO, Dictionary<string, string>? errors)>GetByID(string ID)
        {
            AppointmentDTO? currentAppointment = null;
            var appointment = await _appointment.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            
            if (appointment == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, currentAppointment,response.Errors);
            }
            return (response.Status, response.Response, response.IsSuccess, new AppointmentDTO(appointment.ID,appointment.PatientID,appointment.DoctorID,appointment.Date,appointment.Time,appointment.Duration,appointment.Status,appointment.NotificationSent),response.Errors);
        }
        public async Task<(int Status,string? Response, bool IsSuccess, Dictionary<string, string>? errors)>Delete(string ID)
        {
            var appointment = await _appointment.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (appointment == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess,response.Errors);
            }
            bool IsDeleted = await _appointment.DeleteAsync(appointment);
            if (!IsDeleted)
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, response.Errors);
            }
            return (response.Status, response.Response, response.IsSuccess, response.Errors);
        }
    }
}
