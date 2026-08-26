using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HSMBusiness.Mappers
{
    public class AppointmentMapper
    {
        public enum enMode { Add,Update}
        public async Task<AppointmentDTO> ToDTO(Appointment appointment)
        {
            return new AppointmentDTO(appointment.ID, appointment.PatientID, appointment.DoctorID, appointment.Date, appointment.Time, appointment.Duration, appointment.Status, appointment.NotificationSent);
        }
        public async Task<Appointment> ToEntity(AppointmentDTO appointmentDTO ,enMode mode=enMode.Add,Appointment appointment = null)
        {
            if (mode == enMode.Add)
            {
                return new Appointment
                {
                    ID = appointmentDTO.ID,
                    PatientID=appointmentDTO.PatientID,
                    DoctorID=appointmentDTO.DoctorID,
                    Date=appointmentDTO.Date,
                    Time=appointmentDTO.Time,
                    Duration=appointmentDTO.Duration,
                    Status=appointmentDTO.Status,
                    NotificationSent=appointmentDTO.NotificationSent
                };
            }
            else
            {
                appointment.PatientID = appointmentDTO.PatientID;
                appointment.DoctorID = appointmentDTO.DoctorID;
                appointment.Date = appointmentDTO.Date;
                appointment.Time = appointmentDTO.Time;
                appointment.Duration = appointmentDTO.Duration;
                appointment.Status = appointmentDTO.Status;
                appointment.NotificationSent = appointmentDTO.NotificationSent;
                return appointment;
            }
        }
    }
}
