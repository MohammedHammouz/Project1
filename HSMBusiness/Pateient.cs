using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness
{
    public class Pateient
    {
        public string PatientID { get; set; } = null!;
        public string MedicalHistory { get; set; } = null!;
        public bool Status { get; set; } = false;
        public string PersonID { get; set; } = null!;
        public PatientDTO patientDTO { get
            {
                return new PatientDTO(PatientID, MedicalHistory, Status, PersonID);
            } 
        }
        private readonly PatientRepository _patientRepository;
        public enum enMode { Add,Update}
        public enMode _Mode = enMode.Add;
        public Pateient(PatientDTO patientDTO, PatientRepository patientRepository, enMode Mode=enMode.Add)
        {
            PatientID = patientDTO.PatientID;
            MedicalHistory = patientDTO.MedicalHistory;
            Status = patientDTO.Status;
            PersonID = patientDTO.PersonID;
            _patientRepository = patientRepository;
            _Mode = Mode;
        }
        public async Task<List<PatientDTO>> GetAll()
        {
            var patients = await _patientRepository.GetAllAsync();
            return patients.Select(p => new PatientDTO(p.PatientID, p.MedicalHistory, p.Status, p.PersonID))
                .ToList();
        }
        public async Task<PatientDTO>GetByID(string PatientID)
        {
            var patient = await _patientRepository.GetByIDAsync(PatientID);
            if (patient == null)
            {
                return new PatientDTO("","",false,"");
            }
            return new PatientDTO(patient.PatientID, patient.MedicalHistory, patient.Status, patient.PersonID);
        }
        private async Task<bool> _AddNew()
        {
            PatientEntity? patientEntity = null;
            patientEntity.PersonID = patientDTO.PersonID;
            patientEntity.Status = patientDTO.Status;
            patientEntity.MedicalHistory = patientDTO.MedicalHistory;
            this.PatientID = await _patientRepository.AddAsync(patientEntity);
            return this.PatientID != "";
        }
        private async Task<bool> _Update()
        {
            PatientEntity? patientEntity = null;
            patientEntity.PersonID = patientDTO.PersonID;
            patientEntity.Status = patientDTO.Status;
            patientEntity.MedicalHistory = patientDTO.MedicalHistory;
            return await _patientRepository.UpdateAsync(patientEntity);
        }
        public async Task<bool> Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if(await _AddNew())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return await _Update();
            }
            return false;
        }
        public async Task<bool>Delete(string PatientID)
        {
            PatientDTO patient = await GetByID(PatientID);
            if (patient == null)
            {
                return false;
            }
            PatientEntity? patientEntity = null;
            patientEntity.PatientID = PatientID;
            patientEntity.PersonID = patientDTO.PersonID;
            patientEntity.Status = patientDTO.Status;
            patientEntity.MedicalHistory = patientDTO.MedicalHistory;
            return await _patientRepository.DeleteAsync(patientEntity);
        }
    }
}
