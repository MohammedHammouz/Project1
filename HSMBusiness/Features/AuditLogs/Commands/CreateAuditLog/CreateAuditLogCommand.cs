namespace HSMBusiness.Features.AuditLogs.Commands.CreateAuditLog
{
    public sealed record CreateAuditLogCommand(
        string UserId,
        string Entity,
        string Action,
        DateTime Timestamp,
        string? Details);
}
