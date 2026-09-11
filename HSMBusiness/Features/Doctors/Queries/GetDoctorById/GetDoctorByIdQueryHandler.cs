using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Doctors.Dtos;
using HSMBusiness.Features.Doctors.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Doctors.Queries.GetDoctorById
{
    public class GetDoctorByIdQueryHandler(
        IValidator<GetDoctorByIdQuery> validator,
        DoctorRepository doctorRepository)
    {
        public async Task<Result<DoctorDto>> ExecuteAsync(GetDoctorByIdQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var doctor = await doctorRepository.GetByIDAsync(query.Id);
            if (doctor is null)
            {
                return DoctorErrors.NotFound;
            }

            return doctor.ToDto();
        }
    }
}
