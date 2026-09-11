using FluentValidation;

namespace HSMBusiness.Features.AuditLogs.Commands.DeleteAuditLog
{
    public class DeleteAuditLogCommandValidator : AbstractValidator<DeleteAuditLogCommand>
    {
        public DeleteAuditLogCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Audit log Id is required.");
        }
    }
}
