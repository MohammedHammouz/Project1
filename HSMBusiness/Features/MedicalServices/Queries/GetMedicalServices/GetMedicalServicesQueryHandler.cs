using HSMBusiness.Features.MedicalServices.Dtos;
using HSMBusiness.Features.MedicalServices.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.MedicalServices.Queries.GetMedicalServices
{
    public class GetMedicalServicesQueryHandler(MedicalServiceRepository medicalServiceRepository)
    {
        public async Task<Result<List<MedicalServiceDto>>> ExecuteAsync(GetMedicalServicesQuery query)
        {
            var medicalServices = await medicalServiceRepository.GetAllAsync();

            return medicalServices.ToDtos();
        }
    }
}
