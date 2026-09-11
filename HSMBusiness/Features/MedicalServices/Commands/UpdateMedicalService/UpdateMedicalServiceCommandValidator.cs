using FluentValidation;

namespace HSMBusiness.Features.MedicalServices.Commands.UpdateMedicalService
{
    public class UpdateMedicalServiceCommandValidator : AbstractValidator<UpdateMedicalServiceCommand>
    {
        public UpdateMedicalServiceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Medical service Id is required.");

            RuleFor(x => x.ServiceName)
                .NotEmpty().WithMessage("Service name is required.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Service category is required.");

            RuleFor(x => x.DefaultPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Default price must be greater than or equal to zero.");
        }
    }
}
