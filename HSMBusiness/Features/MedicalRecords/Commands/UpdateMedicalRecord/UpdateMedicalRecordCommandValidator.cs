using FluentValidation;

namespace HSMBusiness.Features.MedicalRecords.Commands.UpdateMedicalRecord
{
    public class UpdateMedicalRecordCommandValidator : AbstractValidator<UpdateMedicalRecordCommand>
    {
        public UpdateMedicalRecordCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Medical record Id is required.");

            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Patient is required.");

            RuleFor(x => x.AccessLevel)
                .NotEmpty().WithMessage("Access level is required.");
        }
    }
}
