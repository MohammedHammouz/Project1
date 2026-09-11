using FluentValidation;

namespace HSMBusiness.Features.AuditLogs.Commands.UpdateAuditLog
{
    public class UpdateAuditLogCommandValidator : AbstractValidator<UpdateAuditLogCommand>
    {
        public UpdateAuditLogCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Audit log Id is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User is required.");

            RuleFor(x => x.Entity)
                .NotEmpty().WithMessage("Entity name is required.");

            RuleFor(x => x.Action)
                .NotEmpty().WithMessage("Action is required.");
        }
    }
}
