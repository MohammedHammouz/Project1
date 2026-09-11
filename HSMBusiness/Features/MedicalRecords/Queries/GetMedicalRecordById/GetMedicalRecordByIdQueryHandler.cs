using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.MedicalRecords.Dtos;
using HSMBusiness.Features.MedicalRecords.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.MedicalRecords.Queries.GetMedicalRecordById
{
    public class GetMedicalRecordByIdQueryHandler(
        IValidator<GetMedicalRecordByIdQuery> validator,
        MedicalRecordRepository medicalRecordRepository)
    {
        public async Task<Result<MedicalRecordDto>> ExecuteAsync(GetMedicalRecordByIdQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var medicalRecord = await medicalRecordRepository.GetByIDAsync(query.Id);
            if (medicalRecord is null)
            {
                return MedicalRecordErrors.NotFound;
            }

            return medicalRecord.ToDto();
        }
    }
}
