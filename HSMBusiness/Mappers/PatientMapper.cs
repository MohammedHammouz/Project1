using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Mappers
{
    public class PatientMapper
    {
        public static PatientDTO ToDTO(Patient patient)
        {
            return new PatientDTO(patient.PatientID, patient.MedicalHistory, patient.Status, patient.PersonID);
        }
        public static PatientEntity ToEntity(PatientDTO patientDTO)
        {
            return new PatientEntity
            {
                ID = patientDTO.ID,
                MedicalHistory = patientDTO.MedicalHistory,
                Status = patientDTO.Status,
                PersonID = patientDTO.PersonID
            };
        }
        public static void LoadDTO(PatientDTO dto, Patient patient)
        {
            dto.ID = patient.PatientID;
            dto.MedicalHistory = patient.MedicalHistory;
            dto.Status = patient.Status;
            dto.PersonID = patient.PersonID;
        }
    }
}
