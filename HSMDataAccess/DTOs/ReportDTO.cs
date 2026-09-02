using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.DTOs
{
    public class ReportDTO
    {
        public string ID { get; set; } = null!;
        public string Type { get; set; } = null!;
        public DateTime GeneratedOn { get; set; }
        public string GeneratedBy { get; set; } = null!;
        public int? AppointmentCount { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? PaymentsReceived { get; set; }
        public decimal? PendingPayments { get; set; }
        public string? Metrics { get; set; }
        public string? ExportFormat { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public ReportDTO()
        {

        }
        public ReportDTO(
        string id,
        string type,
        DateTime generatedOn,
        string generatedBy,
        int? appointmentCount,
        decimal? revenue,
        decimal? paymentsReceived,
        decimal? pendingPayments,
        string? metrics,
        string? exportFormat,
        string? status,
        string? notes)
        {
            ID = id;
            Type = type;
            GeneratedOn = generatedOn;
            GeneratedBy = generatedBy;
            AppointmentCount = appointmentCount;
            Revenue = revenue;
            PaymentsReceived = paymentsReceived;
            PendingPayments = pendingPayments;
            Metrics = metrics;
            ExportFormat = exportFormat;
            Status = status;
            Notes = notes;
        }
    }
}
