using HSMBusiness.Services;
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
        public enum enMode { Add, Update }
        public  PatientDTO ToDTO(Patient patient)
        {
            return new PatientDTO(patient.ID, patient.MedicalHistory, patient.Status, patient.PersonID);
        }
        public Patient ToEntity(PatientDTO patientDTO,enMode mode =enMode.Add,Patient patient=null)
        {
            if (mode == enMode.Add)
            {
                return new Patient
                {
                    MedicalHistory = patientDTO.MedicalHistory,
                    Status = patientDTO.Status,
                    PersonID = patientDTO.PersonID
                };
            }
            else
            {
                patient.MedicalHistory = patientDTO.MedicalHistory;
                patient.Status = patientDTO.Status;
                patient.PersonID = patientDTO.PersonID;
            }
            return patient;
        }
       
    }
}
