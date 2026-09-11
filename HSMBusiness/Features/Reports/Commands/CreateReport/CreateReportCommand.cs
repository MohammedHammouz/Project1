namespace HSMBusiness.Features.Reports.Commands.CreateReport
{
    public sealed record CreateReportCommand(
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
