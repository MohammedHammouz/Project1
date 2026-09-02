using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.DTOs
{
    public class MedicalRecordDTO
    {
        public string ID { get; set; } = null!;
        public string PatientID { get; set; } = null!;
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public string? Prescriptions { get; set; }
        public string? Status { get; set; }
        public string AuditTrail { get; set; } = null!;
        public string AccessLevel { get; set; } = null!;

        public MedicalRecordDTO()
        {

        }
        public MedicalRecordDTO(
            string id,
            string patientID,
            string? diagnosis,
            string? treatment,
            string? prescriptions,
            string? status,
            string auditTrail,
            string accessLevel)
        {
            ID = id;
            PatientID = patientID;
            Diagnosis = diagnosis;
            Treatment = treatment;
            Prescriptions = prescriptions;
            Status = status;
            AuditTrail = auditTrail;
            AccessLevel = accessLevel;
        }
    }

}
