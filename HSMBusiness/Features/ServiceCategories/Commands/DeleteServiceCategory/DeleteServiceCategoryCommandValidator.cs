using FluentValidation;

namespace HSMBusiness.Features.ServiceCategories.Commands.DeleteServiceCategory
{
    public class DeleteServiceCategoryCommandValidator : AbstractValidator<DeleteServiceCategoryCommand>
    {
        public DeleteServiceCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Service category Id is required.");
        }
    }
}
