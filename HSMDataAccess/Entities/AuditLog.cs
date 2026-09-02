using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.Entities
{
    public class AuditLog
    {
        public string ID { get; set; } = null!;
        public string UserID { get; set; } = null!;
        public string Entity { get; set; } = null!;
        public string Action { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public string? Details { get; set; }
        public virtual User user { get; set; } = null!;
    }
}
