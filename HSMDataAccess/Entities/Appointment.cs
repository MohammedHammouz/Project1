using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HSMDataAccess.Entities
{
    public class Appointment
    {
        public string ID { get; set; } = null!;
        public string PatientID { get; set; } = null!;
        public string DoctorID { get; set; } = null!;
        public DateTime Date { get; set; }
        public TimeSpan? Time { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; } = null!;
        public bool? NotificationSent { get; set; }
        public virtual Patient patient { get; set; } = null!;
        public virtual Doctor doctor { get; set; } = null!;
    }
}
