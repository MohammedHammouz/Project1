namespace HSMBusiness.Features.AuditLogs.Commands.UpdateAuditLog
{
    public sealed record UpdateAuditLogCommand(
        string Id,
        string UserId,
        string Entity,
        string Action,
        DateTime Timestamp,
        string? Details);
}
