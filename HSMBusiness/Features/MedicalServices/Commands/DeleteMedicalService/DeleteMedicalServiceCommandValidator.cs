using FluentValidation;

namespace HSMBusiness.Features.MedicalServices.Commands.DeleteMedicalService
{
    public class DeleteMedicalServiceCommandValidator : AbstractValidator<DeleteMedicalServiceCommand>
    {
        public DeleteMedicalServiceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Medical service Id is required.");
        }
    }
}
