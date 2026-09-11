using FluentValidation;

namespace HSMBusiness.Features.ServiceCategories.Commands.UpdateServiceCategory
{
    public class UpdateServiceCategoryCommandValidator : AbstractValidator<UpdateServiceCategoryCommand>
    {
        public UpdateServiceCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Service category Id is required.");

            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
        }
    }
}
