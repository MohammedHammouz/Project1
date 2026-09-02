using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.Entities
{
    public class Report
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
        public virtual User user { get; set; } = null!;
    }
}
