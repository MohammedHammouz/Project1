using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Mappers
{
    public class MedicalRecordMapper
    {
        public enum enMode { Add, Update }

        public MedicalRecord ToEntity(
            MedicalRecordDTO medicalRecordDTO,
            enMode mode = enMode.Add)
        {
            if (mode == enMode.Add)
            {
                return new MedicalRecord
                {
                    ID = medicalRecordDTO.ID,
                    PatientID = medicalRecordDTO.PatientID,
                    Diagnosis = medicalRecordDTO.Diagnosis,
                    Treatment = medicalRecordDTO.Treatment,
                    Prescriptions = medicalRecordDTO.Prescriptions,
                    Status = medicalRecordDTO.Status,
                    AuditTrail = medicalRecordDTO.AuditTrail,
                    AccessLevel = medicalRecordDTO.AccessLevel
                };
            }
            else
            {
                return new MedicalRecord
                {
                    ID = medicalRecordDTO.ID,
                    PatientID = medicalRecordDTO.PatientID,
                    Diagnosis = medicalRecordDTO.Diagnosis,
                    Treatment = medicalRecordDTO.Treatment,
                    Prescriptions = medicalRecordDTO.Prescriptions,
                    Status = medicalRecordDTO.Status,
                    AuditTrail = medicalRecordDTO.AuditTrail,
                    AccessLevel = medicalRecordDTO.AccessLevel
                };
            }
        }

        public MedicalRecordDTO ToDTO(MedicalRecord medicalRecord)
        {
            return new MedicalRecordDTO
            (
                medicalRecord.ID,
                medicalRecord.PatientID,
                medicalRecord.Diagnosis,
                medicalRecord.Treatment,
                medicalRecord.Prescriptions,
                medicalRecord.Status,
                medicalRecord.AuditTrail,
                medicalRecord.AccessLevel
            );
        }
    }
}
