using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.DTOs
{
    public class AppointmentDTO
    {
        public string ID { get; set; } = null!;
        public string PatientID { get; set; } = null!;
        public string DoctorID { get; set; } = null!;
        public DateTime Date { get; set; }
        public TimeSpan? Time { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; } = null!;
        public bool? NotificationSent { get; set; }
        public AppointmentDTO(string id,string patientID,string doctorID,
            DateTime date,TimeSpan? time,int duration,string status,bool? notificationSent)
        {
            ID = id;
            PatientID = patientID;
            DoctorID = doctorID;
            Date = date;
            Time = time;
            Duration = duration;
            Status = status;
            NotificationSent = notificationSent;
        }
        public AppointmentDTO()
        {

        }
    }
}