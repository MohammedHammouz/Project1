using Azure;
using HSMBusiness.Mappers;
using HSMBusiness.Pattern;
using HSMDataAccess.DTOs;
using HSMDataAccess.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Services
{
    public class MedicalRecordService
    {
        ResultPatern resultPattern = new ResultPatern();
        public enum enMode { Add = 0, Update }
        public enMode _mode = enMode.Add;
        private readonly MedicalRecordRepository _medicalRecord;
        private readonly PatientService _patientService;
        public MedicalRecordRepository medicalRecordRepository
        {
            get { return _medicalRecord; }
        }
        public MedicalRecordService(MedicalRecordRepository medicalRecord,PatientService patientService,enMode mode = enMode.Add)
        {
            _medicalRecord = medicalRecord;
            _patientService = patientService;
            _mode = mode;
        }
        private async Task<(int Status,bool IsSuccess)> Add(MedicalRecordDTO medicalRecordDTO)
        {
            var medicalRecordEntity =new MedicalRecordMapper().ToEntity(medicalRecordDTO);
            if(!await _patientService.IsPatientExist(medicalRecordEntity.PatientID))
            {
                return (500, false);
            }
            if (medicalRecordDTO.Status != "Archived" || medicalRecordDTO.Status != "Active")
            {
                return (500,false);
            }
            var AddNew =await _medicalRecord.AddAsync(medicalRecordEntity);
            
            if (AddNew.ID == "")
            {
                return (500, false);
            }
            medicalRecordEntity.ID = AddNew.ID;
            return (200, true);
        }
        private async Task<(int, string?, bool)> Update( string ID, MedicalRecordDTO medicalRecordDTO)
        {
            var CurrentMedicalRecord =await _medicalRecord.GetByIDAsync(ID);
           
            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (CurrentMedicalRecord == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess);
            }
            if (CurrentMedicalRecord.Status != "Archived" || CurrentMedicalRecord.Status != "Active")
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            if (!await _patientService.IsPatientExist(CurrentMedicalRecord.PatientID))
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            CurrentMedicalRecord =new MedicalRecordMapper().ToEntity(medicalRecordDTO,MedicalRecordMapper.enMode.Update);
            return (response.Status, response.Response,await _medicalRecord.UpdateAsync(CurrentMedicalRecord) );
        }
        public async Task<(int Status, string?Response, bool IsSuccess)> Save(MedicalRecordDTO medicalRecordDTO,string ID = "")
        {
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            switch (_mode)
            {
                case enMode.Add:
                    var AddNew = await Add(medicalRecordDTO);
                    if (AddNew.IsSuccess)
                    {
                        _mode = enMode.Update;
                    }
                    else
                    {
                        response =await resultPattern.GiveResponse(AddNew.Status, new Dictionary<string, string>());
                    }
                    return (response.Status,response.Response,response.IsSuccess);
                case enMode.Update:
                    return await Update(ID, medicalRecordDTO);
            }
            response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
            return ( response.Status,response.Response,response.IsSuccess);
        }
        public async Task<(int Status,string? Response,bool IsSuccess,List<MedicalRecordDTO>?ListOfMedicalRecords)> GetAll()
        {
            var medicalRecords =await _medicalRecord.GetAllAsync();
            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (medicalRecords == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess,null);
            }
            return (response.Status,response.Response,response.IsSuccess,medicalRecords.Select(m =>new MedicalRecordMapper().ToDTO(m)).ToList());
        }
        public async Task<(int Status,string?Response,bool IsSuccess,MedicalRecordDTO medicalRecordDTO)> GetByID(string ID)
        {
            var medicalRecord =await _medicalRecord.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (medicalRecord == null)
            {
                response = await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return ( response.Status, response.Response, response.IsSuccess, null );
            }
            return (response.Status,response.Response,response.IsSuccess,new MedicalRecordMapper().ToDTO(medicalRecord));
        }
        public async Task<(int Status, string?Response, bool IsSuccess)> Delete(string ID)
        {
            var medicalRecord =await _medicalRecord.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (medicalRecord == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return ( response.Status,response.Response, response.IsSuccess);
            }
            bool IsDeleted =await _medicalRecord.DeleteAsync(medicalRecord);
            if (!IsDeleted)
            {
                response =await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess);
            }
            return (response.Status,response.Response, response.IsSuccess);
        }
    }
}