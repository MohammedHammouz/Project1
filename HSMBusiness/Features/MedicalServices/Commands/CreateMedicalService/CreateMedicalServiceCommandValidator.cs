using FluentValidation;

namespace HSMBusiness.Features.MedicalServices.Commands.CreateMedicalService
{
    public class CreateMedicalServiceCommandValidator : AbstractValidator<CreateMedicalServiceCommand>
    {
        public CreateMedicalServiceCommandValidator()
        {
            RuleFor(x => x.ServiceName)
                .NotEmpty().WithMessage("Service name is required.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Service category is required.");

            RuleFor(x => x.DefaultPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Default price must be greater than or equal to zero.");
        }
    }
}
