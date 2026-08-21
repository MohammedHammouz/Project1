using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.DTOs
{
    public class PatientDTO
    {
        public string ID { get; set; } = null!;
        public string MedicalHistory { get; set; } = null!;
        public bool Status { get; set; } = false;
        public string PersonID { get; set; } = null!;
        public PatientDTO(string PatientID, string MedicalHistory, bool Status, string PersonID)
        {
            this.ID = PatientID;
            this.MedicalHistory = MedicalHistory;
            this.Status = Status;
            this.PersonID = PersonID;
        }
    }
}
