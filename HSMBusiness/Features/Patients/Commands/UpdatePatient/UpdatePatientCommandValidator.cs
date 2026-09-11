using FluentValidation;

namespace HSMBusiness.Features.Patients.Commands.UpdatePatient
{
    public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Patient Id is required.");

            RuleFor(x => x.PersonId)
                .NotEmpty().WithMessage("Person is required.");

            RuleFor(x => x.MedicalHistory)
                .NotEmpty().WithMessage("Medical history is required.");
        }
    }
}
