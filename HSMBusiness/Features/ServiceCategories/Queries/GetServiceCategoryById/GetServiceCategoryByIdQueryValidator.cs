using FluentValidation;

namespace HSMBusiness.Features.ServiceCategories.Queries.GetServiceCategoryById
{
    public class GetServiceCategoryByIdQueryValidator : AbstractValidator<GetServiceCategoryByIdQuery>
    {
        public GetServiceCategoryByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Service category Id is required.");
        }
    }
}
