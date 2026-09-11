using FluentValidation;

namespace HSMBusiness.Features.AuditLogs.Queries.GetAuditLogById
{
    public class GetAuditLogByIdQueryValidator : AbstractValidator<GetAuditLogByIdQuery>
    {
        public GetAuditLogByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Audit log Id is required.");
        }
    }
}
