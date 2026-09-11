using FluentValidation;

namespace HSMBusiness.Features.Bills.Commands.DeleteBill
{
    public class DeleteBillCommandValidator : AbstractValidator<DeleteBillCommand>
    {
        public DeleteBillCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Bill Id is required.");
        }
    }
}
