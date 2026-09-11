using HSMBusiness.Features.MedicalRecords.Dtos;
using HSMBusiness.Features.MedicalRecords.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.MedicalRecords.Queries.GetMedicalRecords
{
    public class GetMedicalRecordsQueryHandler(MedicalRecordRepository medicalRecordRepository)
    {
        public async Task<Result<List<MedicalRecordDto>>> ExecuteAsync(GetMedicalRecordsQuery query)
        {
            var medicalRecords = await medicalRecordRepository.GetAllAsync();

            return medicalRecords.ToDtos();
        }
    }
}
