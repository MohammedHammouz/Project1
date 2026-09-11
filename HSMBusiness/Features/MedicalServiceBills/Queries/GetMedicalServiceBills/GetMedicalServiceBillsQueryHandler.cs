using HSMBusiness.Features.MedicalServiceBills.Dtos;
using HSMBusiness.Features.MedicalServiceBills.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.MedicalServiceBills.Queries.GetMedicalServiceBills
{
    public class GetMedicalServiceBillsQueryHandler(MedicalServiceBillRepository medicalServiceBillRepository)
    {
        public async Task<Result<List<MedicalServiceBillDto>>> ExecuteAsync(GetMedicalServiceBillsQuery query)
        {
            var medicalServiceBills = await medicalServiceBillRepository.GetAllAsync();

            return medicalServiceBills.ToDtos();
        }
    }
}
