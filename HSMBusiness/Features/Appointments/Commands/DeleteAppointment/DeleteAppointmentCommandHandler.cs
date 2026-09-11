using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Appointments.Commands.DeleteAppointment
{
    public class DeleteAppointmentCommandHandler(
        IValidator<DeleteAppointmentCommand> validator,
        AppointmentRepository appointmentRepository)
    {
        public async Task<Result<Deleted>> ExecuteAsync(DeleteAppointmentCommand command)
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

            var isDeleted = await appointmentRepository.DeleteAsync(appointment);
            if (!isDeleted)
            {
                return Error.Failure("Appointment_DeleteFailed", "Failed to delete appointment.");
            }

            return Result.Deleted;
        }
    }
}
