using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.DTOs
{
    public class NotifictionDTO
    {
        public string ID { get; set; } = null!;

        public string PatientID { get; set; } = null!;

        public string UserID { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string Message { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime? SentOn { get; set; }

        public bool? DeliveryConfirmation { get; set; }

        public NotifictionDTO(
            string id,
            string patientID,
            string userID,
            string type,
            string message,
            string status,
            DateTime? sentOn,
            bool? deliveryConfirmation)
        {
            ID = id;
            PatientID = patientID;
            UserID = userID;
            Type = type;
            Message = message;
            Status = status;
            SentOn = sentOn;
            DeliveryConfirmation = deliveryConfirmation;
        }
    }
}
