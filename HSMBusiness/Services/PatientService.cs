using HSMBusiness.Mappers;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Services
{
    public class PatientService
    {
    
        private readonly PatientRepository _patientRepository;
        public enum enMode { Add, Update }
        public enMode _Mode = enMode.Add;
        public PatientService(PatientRepository patientRepository, enMode Mode = enMode.Add)
        {
            
            _patientRepository = patientRepository;
            _Mode = Mode;
        }
        public async Task<List<PatientDTO>> GetAll()
        {
            var patients = await _patientRepository.GetAllAsync();
            if (patients == null)
            {
                return null;
            }
            return patients.Select(p => new PatientDTO(p.ID, p.MedicalHistory, p.Status, p.PersonID))
                .ToList();
        }
        public async Task<PatientDTO> GetByID(string PatientID)
        {
            var patient = await _patientRepository.GetByIDAsync(PatientID);
            if (patient == null)
            {
                return new PatientDTO("", "", false, "");
            }
            return new PatientDTO(patient.ID, patient.MedicalHistory, patient.Status, patient.PersonID);
        }
        private async Task<bool> _AddNew(PatientDTO patientDTO)
        {
            Patient? patientEntity = new PatientMapper().ToEntity(patientDTO);
            var NewPatient = await _patientRepository.AddAsync(patientEntity);
            patientEntity.ID = NewPatient.ID;
            return patientEntity.ID != "";
        }
        private async Task<bool> _Update(string ID)
        {
            Patient? patientEntity = await _patientRepository.GetByIDAsync(ID);
            if (patientEntity == null)
                return false;
            return await _patientRepository.UpdateAsync(patientEntity);
        }
        public async Task<bool> Save(PatientDTO patientDTO=null,string ID="")
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (await _AddNew(patientDTO))
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return await _Update(ID);
            }
            return false;
        }
        public async Task<bool> Delete(string PatientID)
        {
            var patient = await _patientRepository.GetByIDAsync(PatientID);
            if (patient == null)
            {
                return false;
            }
            return await _patientRepository.DeleteAsync(patient);
        }
    }
}
