using FluentValidation;

namespace HSMBusiness.Features.Reports.Commands.DeleteReport
{
    public class DeleteReportCommandValidator : AbstractValidator<DeleteReportCommand>
    {
        public DeleteReportCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Report Id is required.");
        }
    }
}
