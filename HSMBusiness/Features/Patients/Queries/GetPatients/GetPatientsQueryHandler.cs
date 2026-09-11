using HSMBusiness.Features.Patients.Dtos;
using HSMBusiness.Features.Patients.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Patients.Queries.GetPatients
{
    public class GetPatientsQueryHandler(PatientRepository patientRepository)
    {
        public async Task<Result<List<PatientDto>>> ExecuteAsync(GetPatientsQuery query)
        {
            var patients = await patientRepository.GetAllAsync();

            return patients.ToDtos();
        }
    }
}
