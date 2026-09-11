using FluentValidation;

namespace HSMBusiness.Features.Patients.Commands.CreatePatient
{
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
            RuleFor(x => x.PersonId)
                .NotEmpty().WithMessage("Person is required.");

            RuleFor(x => x.MedicalHistory)
                .NotEmpty().WithMessage("Medical history is required.");
        }
    }
}
