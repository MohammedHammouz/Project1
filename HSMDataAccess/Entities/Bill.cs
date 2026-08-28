using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.Entities
{
    public class Bill
    {
        public string ID { get; set; } = null!;
        public string PatientID { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime? Date { get; set; }
        public decimal? PartialPaymentAmount { get; set; }
        public DateTime? DueDate { get; set; }
        public string Invoice { get; set; } = null!;
        public decimal GrossAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? InsuranceCoverage { get; set; }
        public decimal PatientResponsibility { get; set; }
        public virtual Patient patient { get; set; } = null!;
    }
}
