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
        public  PatientDTO ToDTO(Patient patient)
        {
            return new PatientDTO(patient.ID, patient.MedicalHistory, patient.Status, patient.PersonID);
        }
        public  Patient ToEntity(PatientDTO patientDTO)
        {
            return new Patient
            {
                ID = patientDTO.ID,
                MedicalHistory = patientDTO.MedicalHistory,
                Status = patientDTO.Status,
                PersonID = patientDTO.PersonID
            };
        }
       
    }
}
