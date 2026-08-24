using HSMBusiness.Mappers;
using HSMBusiness.Pattern;
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
        ResultPatern resultPattern = new ResultPatern();
        private readonly PatientRepository _patientRepository;
        public enum enMode { Add, Update }
        public enMode _Mode = enMode.Add;
        public PatientService(PatientRepository patientRepository, enMode Mode = enMode.Add)
        {
            _patientRepository = patientRepository;
            _Mode = Mode;
        }
        public async Task<(int,string?,bool,List<PatientDTO>)> GetAll()
        {
            var patients = await _patientRepository.GetAllAsync();
            var response = await resultPattern.GiveResponse(200);
            if (patients == null)
            {
                response = await resultPattern.GiveResponse(404);
                return (response.Status, response.Response, response.IsSuccess, null);
            }
            return (response.Status, response.Response, response.IsSuccess, patients.Select(p => new PatientDTO(p.ID, p.MedicalHistory, p.Status, p.PersonID))
                .ToList());
        }
        public async Task<(int, string?, bool, PatientDTO)> GetByID(string PatientID)
        {
            var patient = await _patientRepository.GetByIDAsync(PatientID);
            var response = await resultPattern.GiveResponse(200);
            if (patient == null)
            {
                response = await resultPattern.GiveResponse(404);
                return (response.Status, response.Response, response.IsSuccess, new PatientDTO("", "", false, ""));
            }
            return (response.Status, response.Response, response.IsSuccess, new PatientDTO(patient.ID, patient.MedicalHistory, patient.Status, patient.PersonID));
        }
        private async Task<bool> _AddNew(PatientDTO patientDTO)
        {
            Patient? patientEntity = new PatientMapper().ToEntity(patientDTO);
            var NewPatient = await _patientRepository.AddAsync(patientEntity);
            patientEntity.ID = NewPatient.ID;
            return patientEntity.ID != "";
        }
        private async Task<(int, string?, bool)> _Update(string ID)
        {
            Patient? patientEntity = await _patientRepository.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200);
            if (patientEntity == null)
            {
                response = await resultPattern.GiveResponse(404);
                return (response.Status, response.Response, response.IsSuccess);
            }
           
            return (response.Status, response.Response, await _patientRepository.UpdateAsync(patientEntity));
        }
        public async Task<(int, string?, bool)> Save(PatientDTO patientDTO=null,string ID="")
        {
            var response = await resultPattern.GiveResponse(200);
            switch (_Mode)
            {
                case enMode.Add:
                    if (await _AddNew(patientDTO))
                    {
                        _Mode = enMode.Update;
                        return (response.Status, response.Response,response.IsSuccess);
                    }
                    else
                    {
                        response = await resultPattern.GiveResponse(500);
                        return (response.Status, response.Response, response.IsSuccess);
                    }
                case enMode.Update:
                    return await _Update(ID);
            }
            response = await resultPattern.GiveResponse(500);
            return (response.Status, response.Response, response.IsSuccess);
        }
        public async Task<(int, string?, bool)> Delete(string PatientID)
        {
            var patient = await _patientRepository.GetByIDAsync(PatientID);
            var response = await resultPattern.GiveResponse(200);
            if (patient == null)
            {
                response = await resultPattern.GiveResponse(404);
                return (response.Status, response.Response, response.IsSuccess);
            }
            bool IsDeleted = await _patientRepository.DeleteAsync(patient);
            if (!IsDeleted)
            {
                response = await resultPattern.GiveResponse(500);
                return (response.Status, response.Response, response.IsSuccess);
            }
            return (response.Status, response.Response, response.IsSuccess);
        }
    }
}
