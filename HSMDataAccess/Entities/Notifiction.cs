using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HSMDataAccess.Entities
{
    public class Notifiction
    {
        public string ID { get; set; } = null!;

        public string PatientID { get; set; } = null!;

        public string UserID { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string Message { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime? SentOn { get; set; }

        public bool? DeliveryConfirmation { get; set; }

        // Navigation Property
        public virtual User User { get; set; } = null!;
        public virtual Patient patient { get; set; } = null!;


        
    }
}
