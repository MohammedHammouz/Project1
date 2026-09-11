using FluentValidation;

namespace HSMBusiness.Features.Reports.Commands.UpdateReport
{
    public class UpdateReportCommandValidator : AbstractValidator<UpdateReportCommand>
    {
        public UpdateReportCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Report Id is required.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Report type is required.");

            RuleFor(x => x.GeneratedBy)
                .NotEmpty().WithMessage("GeneratedBy user is required.");
        }
    }
}
