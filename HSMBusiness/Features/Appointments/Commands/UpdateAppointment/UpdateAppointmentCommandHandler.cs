using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Appointments.Dtos;
using HSMBusiness.Features.Appointments.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Appointments.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommandHandler(
        IValidator<UpdateAppointmentCommand> validator,
        AppointmentRepository appointmentRepository,
        PatientRepository patientRepository,
        DoctorRepository doctorRepository)
    {
        public async Task<Result<AppointmentDto>> ExecuteAsync(UpdateAppointmentCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var appointment = await appointmentRepository.GetByIDAsync(command.Id);
            if (appointment is null)
            {
                return AppointmentErrors.NotFound;
            }

            var patient = await patientRepository.GetByIDAsync(command.PatientId);
            if (patient is null)
            {
                return AppointmentErrors.PatientNotFound;
            }

            var doctor = await doctorRepository.GetByIDAsync(command.DoctorId);
            if (doctor is null)
            {
                return AppointmentErrors.DoctorNotFound;
            }

            appointment.PatientID = command.PatientId;
            appointment.DoctorID = command.DoctorId;
            appointment.Date = command.Date;
            appointment.Time = command.Time;
            appointment.Duration = command.Duration;
            appointment.Status = command.Status;
            appointment.NotificationSent = command.NotificationSent;

            await appointmentRepository.UpdateAsync(appointment);

            return appointment.ToDto();
        }
    }
}
