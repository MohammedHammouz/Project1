using FluentValidation;

namespace HSMBusiness.Features.Appointments.Queries.GetAppointmentById
{
    public class GetAppointmentByIdQueryValidator : AbstractValidator<GetAppointmentByIdQuery>
    {
        public GetAppointmentByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Appointment Id is required.");
        }
    }
}
