using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.MedicalServices.Dtos;
using HSMBusiness.Features.MedicalServices.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.MedicalServices.Queries.GetMedicalServiceById
{
    public class GetMedicalServiceByIdQueryHandler(
        IValidator<GetMedicalServiceByIdQuery> validator,
        MedicalServiceRepository medicalServiceRepository)
    {
        public async Task<Result<MedicalServiceDto>> ExecuteAsync(GetMedicalServiceByIdQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var medicalService = await medicalServiceRepository.GetByIDAsync(query.Id);
            if (medicalService is null)
            {
                return MedicalServiceErrors.NotFound;
            }

            return medicalService.ToDto();
        }
    }
}
