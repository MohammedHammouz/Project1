using HSMBusiness.Mappers;
using HSMBusiness.Pattern;
using HSMDataAccess.DTOs;
using HSMDataAccess.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public MedicalRecordRepository medicalRecordRepository
        {
            get { return _medicalRecord; }
        }

        public MedicalRecordService(
            MedicalRecordRepository medicalRecord,
            enMode mode = enMode.Add)
        {
            _medicalRecord = medicalRecord;
            _mode = mode;
        }

        private async Task<bool> Add(MedicalRecordDTO medicalRecordDTO)
        {
            var medicalRecordEntity =
                new MedicalRecordMapper().ToEntity(medicalRecordDTO);

            var AddNew =
                await _medicalRecord.AddAsync(medicalRecordEntity);

            medicalRecordEntity.ID = AddNew.ID;

            return medicalRecordEntity.ID != "";
        }

        private async Task<(int, string?, bool)> Update(
            string ID,
            MedicalRecordDTO medicalRecordDTO)
        {
            var CurrentMedicalRecord =
                await _medicalRecord.GetByIDAsync(ID);

            var response =
                await resultPattern.GiveResponse(200);

            if (CurrentMedicalRecord == null)
            {
                response =
                    await resultPattern.GiveResponse(404);

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess
                );
            }

            CurrentMedicalRecord =
                new MedicalRecordMapper().ToEntity(
                    medicalRecordDTO,
                    MedicalRecordMapper.enMode.Update
                );

            return (
                response.Status,
                response.Response,
                await _medicalRecord.UpdateAsync(CurrentMedicalRecord)
            );
        }

        public async Task<(int, string?, bool)> Save(
            MedicalRecordDTO medicalRecordDTO,
            string ID = "")
        {
            var response =
                await resultPattern.GiveResponse(200);

            switch (_mode)
            {
                case enMode.Add:

                    if (await Add(medicalRecordDTO))
                    {
                        _mode = enMode.Update;
                    }
                    else
                    {
                        response =
                            await resultPattern.GiveResponse(400);
                    }

                    return (
                        response.Status,
                        response.Response,
                        response.IsSuccess
                    );

                case enMode.Update:

                    return await Update(ID, medicalRecordDTO);
            }

            response =
                await resultPattern.GiveResponse(500);

            return (
                response.Status,
                response.Response,
                response.IsSuccess
            );
        }

        public async Task<(
            int,
            string?,
            bool,
            List<MedicalRecordDTO>
        )> GetAll()
        {
            var medicalRecords =
                await _medicalRecord.GetAllAsync();

            var response =
                await resultPattern.GiveResponse(200);

            if (medicalRecords == null)
            {
                response =
                    await resultPattern.GiveResponse(404);

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess,
                    null
                );
            }

            return (
                response.Status,
                response.Response,
                response.IsSuccess,
                medicalRecords.Select(m =>
                    new MedicalRecordDTO(
                        m.ID,
                        m.PatientID,
                        m.Diagnosis,
                        m.Treatment,
                        m.Prescriptions,
                        m.Status,
                        m.AuditTrail,
                        m.AccessLevel
                    )
                ).ToList()
            );
        }

        public async Task<(
            int,
            string?,
            bool,
            MedicalRecordDTO
        )> GetByID(string ID)
        {
            var medicalRecord =
                await _medicalRecord.GetByIDAsync(ID);

            var response =
                await resultPattern.GiveResponse(200);

            if (medicalRecord == null)
            {
                response =
                    await resultPattern.GiveResponse(404);

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess,
                    new MedicalRecordDTO(
                        "",
                        "",
                        null,
                        null,
                        null,
                        null,
                        "",
                        ""
                    )
                );
            }

            return (
                response.Status,
                response.Response,
                response.IsSuccess,
                new MedicalRecordDTO(
                    medicalRecord.ID,
                    medicalRecord.PatientID,
                    medicalRecord.Diagnosis,
                    medicalRecord.Treatment,
                    medicalRecord.Prescriptions,
                    medicalRecord.Status,
                    medicalRecord.AuditTrail,
                    medicalRecord.AccessLevel
                )
            );
        }

        public async Task<(int, string?, bool)> Delete(string ID)
        {
            var medicalRecord =
                await _medicalRecord.GetByIDAsync(ID);

            var response =
                await resultPattern.GiveResponse(200);

            if (medicalRecord == null)
            {
                response =
                    await resultPattern.GiveResponse(404);

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess
                );
            }

            bool IsDeleted =
                await _medicalRecord.DeleteAsync(medicalRecord);

            if (!IsDeleted)
            {
                response =
                    await resultPattern.GiveResponse(400);

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess
                );
            }

            return (
                response.Status,
                response.Response,
                response.IsSuccess
            );
        }
    }
}