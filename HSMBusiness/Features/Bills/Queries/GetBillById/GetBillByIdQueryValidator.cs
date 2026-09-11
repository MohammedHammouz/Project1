using FluentValidation;

namespace HSMBusiness.Features.Bills.Queries.GetBillById
{
    public class GetBillByIdQueryValidator : AbstractValidator<GetBillByIdQuery>
    {
        public GetBillByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Bill Id is required.");
        }
    }
}
