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
        private readonly PersonService _personService;
        public enum enMode { Add, Update }
        public enMode _Mode = enMode.Add;
        public PatientService(PatientRepository patientRepository,PersonService personService, enMode Mode = enMode.Add)
        {
            _patientRepository = patientRepository;
            _personService = personService;
            _Mode = Mode;
        }
        public async Task<(int Status,string?Response,bool IsSuccess,List<PatientDTO>?ListOfPatients)> GetAll()
        {
            var patients = await _patientRepository.GetAllAsync();
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (patients == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, null);
            }
            return (response.Status, response.Response, response.IsSuccess, patients.Select(p => new PatientDTO(p.ID, p.MedicalHistory, p.Status, p.PersonID))
                .ToList());
        }
        public async Task<(int Status, string?Response, bool IsSuccess, PatientDTO? patientDTO1)> GetByID(string PatientID)
        {
            var patient = await _patientRepository.GetByIDAsync(PatientID);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (patient == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess, null);
            }
            return (response.Status, response.Response, response.IsSuccess, new PatientDTO(patient.ID, patient.MedicalHistory, patient.Status, patient.PersonID));
        }
        private async Task<bool> CountNumberOfPersonIsOne(string ID)
        {
            return await _patientRepository.CountNumberOfPersonIsOne(ID) == 1;
        }
        private async Task<(int Status,bool IsSuccess)> _AddNew(PatientDTO patientDTO)
        {
            Patient? patientEntity =new PatientMapper().ToEntity(patientDTO);
            if(!await _personService.IsPersonExist(patientEntity.PersonID))
            {
                return (500, false);
            }
            if(await CountNumberOfPersonIsOne(patientDTO.PersonID))
            {
                return (500, false);
            }
            var NewPatient = await _patientRepository.AddAsync(patientEntity);
            patientEntity.ID = NewPatient.ID;
            return (200,patientEntity.ID != "");
        }
        private async Task<(int Status, string?Response, bool IsSuccess)> _Update(string ID,PatientDTO patientDTO)
        {
            Patient? patientEntity = await _patientRepository.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (patientEntity == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            if (!await _personService.IsPersonExist(patientDTO.PersonID))
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            if (patientEntity.PersonID.Trim() == patientDTO.PersonID)
            {
                
            }
            else if (patientEntity.PersonID != patientDTO.PersonID)
            {
                if (await CountNumberOfPersonIsOne(patientDTO.PersonID))
                {
                    response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                    return (response.Status, response.Response, response.IsSuccess);
                }
            }
            patientEntity =new PatientMapper().ToEntity(patientDTO,PatientMapper.enMode.Update,patientEntity);
            return (response.Status, response.Response, await _patientRepository.UpdateAsync(patientEntity));
        }
        public async Task<(int Status, string?Response, bool IsSuccess)> Save(PatientDTO? patientDTO,string ID="")
        {
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            switch (_Mode)
            {
                case enMode.Add:
                    var AddNew = await _AddNew(patientDTO);
                    if (AddNew.IsSuccess)
                    {
                        _Mode = enMode.Update;
                        return (response.Status, response.Response,response.IsSuccess);
                    }
                    else
                    {
                        response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                        return (response.Status, response.Response, response.IsSuccess);
                    }
                case enMode.Update:
                    return await _Update(ID,patientDTO);
            }
            response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
            return (response.Status, response.Response, response.IsSuccess);
        }
        public async Task<(int Status, string?Response, bool IsSuccess)> Delete(string PatientID)
        {
            var patient = await _patientRepository.GetByIDAsync(PatientID);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (patient == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            bool IsDeleted = await _patientRepository.DeleteAsync(patient);
            if (!IsDeleted)
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            return (response.Status, response.Response, response.IsSuccess);
        }
        public async Task<bool> IsPatientExist(string ID)
        {
            var patient = await GetByID(ID);
            if (patient.patientDTO1 == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> IsPatientExist(Appointment appointmentEntity, AppointmentDTO appointmentDTO)
        {
            var patient = await GetByID(appointmentEntity.PatientID);
            if (patient.patientDTO1 == null)
            {
                return false;
            }
            return true;
        }
    }
}
