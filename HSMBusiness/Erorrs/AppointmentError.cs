using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Erorrs
{
    public class AppointmentError
    {
        private readonly PatientService _patientService;
        private readonly DoctorService _doctorService;

        public AppointmentError(
            PatientService patientService,
            DoctorService doctorService)
        {
            _patientService = patientService;
            _doctorService = doctorService;
        }

        public async Task<Dictionary<string, string>> ValidateAppointment(
            AppointmentDTO dto)
        {
            var errors = new Dictionary<string, string>();

            // AppointmentID
            if (string.IsNullOrWhiteSpace(dto.ID))
            {
                errors["AppointmentID"] = "AppointmentID is required.";
            }

            // PatientID
            if (string.IsNullOrWhiteSpace(dto.PatientID))
            {
                errors["PatientID"] = "PatientID is required.";
            }
            else
            {
                var patient = await _patientService.GetByID(dto.PatientID);

                if (patient.Item4 == null)
                {
                    errors["PatientID"] = "Patient does not exist.";
                }
            }

            // DoctorID
            if (string.IsNullOrWhiteSpace(dto.DoctorID))
            {
                errors["DoctorID"] = "DoctorID is required.";
            }
            else
            {
                var doctor = await _doctorService.GetDoctorByID(dto.DoctorID);

                if (doctor.Item4 == null)
                {
                    errors["DoctorID"] = "Doctor does not exist.";
                }
            }

            // Date
            if (dto.Date == default)
            {
                errors["Date"] = "Date is required.";
            }

            // Duration
            if (dto.Duration <= 0)
            {
                errors["Duration"] = "Duration must be greater than 0.";
            }

            // Status
            if (string.IsNullOrWhiteSpace(dto.Status))
            {
                errors["Status"] = "Status is required.";
            }
            else
            {
                string[] allowedStatuses =
                {
                    "Cancelled",
                    "Rescheduled",
                    "Scheduled"
                };

                if (!allowedStatuses.Contains(dto.Status))
                {
                    errors["Status"] =
                        "Status must be Cancelled, Rescheduled, or Scheduled.";
                }
            }

            return errors;
        }
    }
}
