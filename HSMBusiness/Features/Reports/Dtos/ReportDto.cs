namespace HSMBusiness.Features.Reports.Dtos
{
    public class ReportDto
    {
        public string Id { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime GeneratedOn { get; set; }
        public string GeneratedBy { get; set; } = string.Empty;
        public int? AppointmentCount { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? PaymentsReceived { get; set; }
        public decimal? PendingPayments { get; set; }
        public string? Metrics { get; set; }
        public string? ExportFormat { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
    }
}
