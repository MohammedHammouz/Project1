namespace HSMBusiness.Features.Reports.Commands.UpdateReport
{
    public sealed record UpdateReportCommand(
        string Id,
        string Type,
        DateTime GeneratedOn,
        string GeneratedBy,
        int? AppointmentCount,
        decimal? Revenue,
        decimal? PaymentsReceived,
        decimal? PendingPayments,
        string? Metrics,
        string? ExportFormat,
        string? Status,
        string? Notes);
}
