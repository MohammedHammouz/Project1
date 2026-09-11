using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Appointments.Dtos;
using HSMBusiness.Features.Appointments.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Appointments.Queries.GetAppointmentById
{
    public class GetAppointmentByIdQueryHandler(
        IValidator<GetAppointmentByIdQuery> validator,
        AppointmentRepository appointmentRepository)
    {
        public async Task<Result<AppointmentDto>> ExecuteAsync(GetAppointmentByIdQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var appointment = await appointmentRepository.GetByIDAsync(query.Id);
            if (appointment is null)
            {
                return AppointmentErrors.NotFound;
            }

            return appointment.ToDto();
        }
    }
}
