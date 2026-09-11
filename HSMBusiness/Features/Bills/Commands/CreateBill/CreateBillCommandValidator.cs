using FluentValidation;

namespace HSMBusiness.Features.Bills.Commands.CreateBill
{
    public class CreateBillCommandValidator : AbstractValidator<CreateBillCommand>
    {
        public CreateBillCommandValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Patient is required.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Bill status is required.");

            RuleFor(x => x.Invoice)
                .NotEmpty().WithMessage("Invoice number is required.");

            RuleFor(x => x.GrossAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Gross amount must be greater than or equal to zero.");
        }
    }
}
