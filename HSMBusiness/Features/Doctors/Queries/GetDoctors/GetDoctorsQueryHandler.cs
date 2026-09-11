using HSMBusiness.Features.Doctors.Dtos;
using HSMBusiness.Features.Doctors.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Doctors.Queries.GetDoctors
{
    public class GetDoctorsQueryHandler(DoctorRepository doctorRepository)
    {
        public async Task<Result<List<DoctorDto>>> ExecuteAsync(GetDoctorsQuery query)
        {
            var doctors = await doctorRepository.GetAllAsync();

            return doctors.ToDtos();
        }
    }
}
