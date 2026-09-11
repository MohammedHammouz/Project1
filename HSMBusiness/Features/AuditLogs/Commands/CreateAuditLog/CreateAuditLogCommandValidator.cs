using FluentValidation;

namespace HSMBusiness.Features.AuditLogs.Commands.CreateAuditLog
{
    public class CreateAuditLogCommandValidator : AbstractValidator<CreateAuditLogCommand>
    {
        public CreateAuditLogCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User is required.");

            RuleFor(x => x.Entity)
                .NotEmpty().WithMessage("Entity name is required.");

            RuleFor(x => x.Action)
                .NotEmpty().WithMessage("Action is required.");
        }
    }
}
