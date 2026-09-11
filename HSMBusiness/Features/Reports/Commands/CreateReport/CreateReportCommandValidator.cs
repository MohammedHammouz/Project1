using FluentValidation;

namespace HSMBusiness.Features.Reports.Commands.CreateReport
{
    public class CreateReportCommandValidator : AbstractValidator<CreateReportCommand>
    {
        public CreateReportCommandValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Report type is required.");

            RuleFor(x => x.GeneratedBy)
                .NotEmpty().WithMessage("GeneratedBy user is required.");
        }
    }
}
