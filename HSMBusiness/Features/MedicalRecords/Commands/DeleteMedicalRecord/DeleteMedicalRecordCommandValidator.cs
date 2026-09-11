using FluentValidation;

namespace HSMBusiness.Features.MedicalRecords.Commands.DeleteMedicalRecord
{
    public class DeleteMedicalRecordCommandValidator : AbstractValidator<DeleteMedicalRecordCommand>
    {
        public DeleteMedicalRecordCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Medical record Id is required.");
        }
    }
}
