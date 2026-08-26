using HSMBusiness.Mappers;
using HSMBusiness.Pattern;
using HSMDataAccess.DTOs;
using HSMDataAccess.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Services
{
    public class AppointmentService
    {
        ResultPatern resultPattern = new ResultPatern();
        public enum enMode { Add = 0, Update }
        public enMode _mode = enMode.Add;
        private readonly AppointmentRepository _appointment;
        public AppointmentRepository doctorRepository { get { return _appointment; } }
        public AppointmentService(AppointmentRepository appointment,enMode mode=enMode.Add)
        {
            _appointment = appointment;
            _mode = mode;
        }
        private async Task<bool> Add(AppointmentDTO appointmentDTO)
        {
            var appointmentEntity = await new AppointmentMapper().ToEntity(appointmentDTO);
            var AddNew = await _appointment.AddAsync(appointmentEntity);
            appointmentEntity.ID = AddNew.ID;
            return appointmentEntity.ID != "";
        }
        private async Task<(int,string?,bool)>Update(string ID,AppointmentDTO appointmentDTO)
        {
            var CurrentAppointment = await _appointment.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200);
            if (CurrentAppointment == null)
            {
                response = await resultPattern.GiveResponse(200);
                return (response.Status, response.Response, response.IsSuccess);
            }
            CurrentAppointment = await new AppointmentMapper().ToEntity(appointmentDTO,AppointmentMapper.enMode.Update,CurrentAppointment);
            return (response.Status, response.Response, await _appointment.UpdateAsync(CurrentAppointment));
        }
        public async Task<(int, string?, bool)>Save(AppointmentDTO appointmentDTO,string ID = "")
        {
            var response = await resultPattern.GiveResponse(200);
            switch (_mode)
            {
                 case enMode.Add:
                    if(await Add(appointmentDTO))
                    {
                        _mode = enMode.Update;
                        await Add(appointmentDTO);
                    }
                    else
                    {
                        response = await resultPattern.GiveResponse(400);
                    }
                    return (response.Status, response.Response, response.IsSuccess);
                 case enMode.Update:
                    return await Update(ID, appointmentDTO);
            }
            response = await resultPattern.GiveResponse(500);
            return (response.Status, response.Response, response.IsSuccess);
        }
        public async Task<(int,string?, bool, List<AppointmentDTO>)> GetAll()
        {
            var appointments = await _appointment.GetAllAsync();
            var response =await resultPattern.GiveResponse(200);
            if (appointments == null)
            {
                response =await resultPattern.GiveResponse(404);
                return (response.Status,response.Response, response.IsSuccess, null);
            }
            return (response.Status, response.Response, response.IsSuccess, appointments.Select(a=> new AppointmentDTO(a.ID,a.PatientID,a.DoctorID,a.Date,a.Time,a.Duration,a.Status,a.NotificationSent)).ToList());
        }
        public async Task<(int,string?, bool, AppointmentDTO)>GetByID(string ID)
        {
            var appointment = await _appointment.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200);
            if (appointment == null)
            {
                response = await resultPattern.GiveResponse(404);
                return (response.Status, response.Response, response.IsSuccess, new AppointmentDTO("", "", "", DateTime.Now, TimeSpan.Zero, 0, "", false));
            }
            return (response.Status, response.Response, response.IsSuccess, new AppointmentDTO(appointment.ID,appointment.PatientID,appointment.DoctorID,appointment.Date,appointment.Time,appointment.Duration,appointment.Status,appointment.NotificationSent));
        }
        public async Task<(int,string?, bool)>Delete(string ID)
        {
            var appointment = await _appointment.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200);
            if (appointment == null)
            {
                response = await resultPattern.GiveResponse(404);
                return (response.Status, response.Response, response.IsSuccess);
            }
            bool IsDeleted = await _appointment.DeleteAsync(appointment);
            if (!IsDeleted)
            {
                response = await resultPattern.GiveResponse(400);
                return (response.Status, response.Response, response.IsSuccess);
            }
            return (response.Status, response.Response, response.IsSuccess);
        }
    }
}
