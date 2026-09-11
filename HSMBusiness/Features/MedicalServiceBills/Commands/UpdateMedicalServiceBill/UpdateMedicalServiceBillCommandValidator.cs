using FluentValidation;

namespace HSMBusiness.Features.MedicalServiceBills.Commands.UpdateMedicalServiceBill
{
    public class UpdateMedicalServiceBillCommandValidator : AbstractValidator<UpdateMedicalServiceBillCommand>
    {
        public UpdateMedicalServiceBillCommandValidator()
        {
            RuleFor(x => x.ServiceId)
                .NotEmpty().WithMessage("Medical service is required.");

            RuleFor(x => x.BillId)
                .NotEmpty().WithMessage("Bill is required.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be greater than or equal to zero.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).When(x => x.Quantity.HasValue)
                .WithMessage("Quantity must be greater than zero.");
        }
    }
}
