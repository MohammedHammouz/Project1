using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Patients.Dtos;
using HSMBusiness.Features.Patients.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Patients.Queries.GetPatientById
{
    public class GetPatientByIdQueryHandler(
        IValidator<GetPatientByIdQuery> validator,
        PatientRepository patientRepository)
    {
        public async Task<Result<PatientDto>> ExecuteAsync(GetPatientByIdQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var patient = await patientRepository.GetByIDAsync(query.Id);
            if (patient is null)
            {
                return PatientErrors.NotFound;
            }

            return patient.ToDto();
        }
    }
}
