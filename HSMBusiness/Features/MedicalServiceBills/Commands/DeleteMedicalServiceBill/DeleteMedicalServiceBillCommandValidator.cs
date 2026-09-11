using FluentValidation;

namespace HSMBusiness.Features.MedicalServiceBills.Commands.DeleteMedicalServiceBill
{
    public class DeleteMedicalServiceBillCommandValidator : AbstractValidator<DeleteMedicalServiceBillCommand>
    {
        public DeleteMedicalServiceBillCommandValidator()
        {
            RuleFor(x => x.ServiceId)
                .NotEmpty().WithMessage("Medical service is required.");

            RuleFor(x => x.BillId)
                .NotEmpty().WithMessage("Bill is required.");
        }
    }
}
