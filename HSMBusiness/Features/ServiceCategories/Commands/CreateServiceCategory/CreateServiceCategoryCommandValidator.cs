using FluentValidation;

namespace HSMBusiness.Features.ServiceCategories.Commands.CreateServiceCategory
{
    public class CreateServiceCategoryCommandValidator : AbstractValidator<CreateServiceCategoryCommand>
    {
        public CreateServiceCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
        }
    }
}
