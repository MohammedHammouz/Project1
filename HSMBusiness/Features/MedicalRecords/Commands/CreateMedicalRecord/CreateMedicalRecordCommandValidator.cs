using FluentValidation;

namespace HSMBusiness.Features.MedicalRecords.Commands.CreateMedicalRecord
{
    public class CreateMedicalRecordCommandValidator : AbstractValidator<CreateMedicalRecordCommand>
    {
        public CreateMedicalRecordCommandValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Patient is required.");

            RuleFor(x => x.AccessLevel)
                .NotEmpty().WithMessage("Access level is required.");
        }
    }
}
