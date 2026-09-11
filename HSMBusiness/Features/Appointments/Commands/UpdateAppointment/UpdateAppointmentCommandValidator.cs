using FluentValidation;

namespace HSMBusiness.Features.Appointments.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Appointment Id is required.");

            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Patient is required.");

            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Doctor is required.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Appointment date is required.");

            RuleFor(x => x.Duration)
                .GreaterThan(0).WithMessage("Duration must be greater than zero.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Appointment status is required.");
        }
    }
}
