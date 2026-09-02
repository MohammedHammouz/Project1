using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.Entities
{
    public class BillService
    {
        public string ServiceID { get; set; } = null!;
        public string BillID { get; set; } = null!;
        public int? Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public virtual Bill bill { get; set; } = null!;
        public virtual MedicalService medicalService { get; set; } = null!;
    }
}
