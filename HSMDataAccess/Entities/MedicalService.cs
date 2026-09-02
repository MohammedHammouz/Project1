using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.Entities
{
    public class MedicalService
    {
        public string ID { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public string CategoryID { get; set; } = null!;
        public decimal DefaultPrice { get; set; }
        public string? Description { get; set; }
        public int? DurationMinutes { get; set; }
        public virtual ServiceCategory serviceCategory { get; set; } = null!;
        public virtual ICollection<BillService> billServices { get; set; }
       = new List<BillService>();
    }
}
