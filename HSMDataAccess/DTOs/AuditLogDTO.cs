using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.DTOs
{
    public class AuditLogDTO
    {
        public string ID { get; set; } = null!;
        public string UserID { get; set; } = null!;
        public string Entity { get; set; } = null!;
        public string Action { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public string? Details { get; set; }

        public AuditLogDTO()
        {
        }

        public AuditLogDTO(
            string id,
            string userID,
            string entity,
            string action,
            DateTime timestamp,
            string? details)
        {
            ID = id;
            UserID = userID;
            Entity = entity;
            Action = action;
            Timestamp = timestamp;
            Details = details;
        }
    }
}
