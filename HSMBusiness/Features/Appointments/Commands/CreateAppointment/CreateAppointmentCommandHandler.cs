using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Appointments.Dtos;
using HSMBusiness.Features.Appointments.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommandHandler(
        IValidator<CreateAppointmentCommand> validator,
        AppointmentRepository appointmentRepository,
        PatientRepository patientRepository,
        DoctorRepository doctorRepository)
    {
        public async Task<Result<AppointmentDto>> ExecuteAsync(CreateAppointmentCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
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

            var appointment = new Appointment
            {
                PatientID = command.PatientId,
                DoctorID = command.DoctorId,
                Date = command.Date,
                Time = command.Time,
                Duration = command.Duration,
                Status = command.Status,
                NotificationSent = command.NotificationSent
            };

            var created = await appointmentRepository.AddAsync(appointment);

            return created.ToDto();
        }
    }
}
