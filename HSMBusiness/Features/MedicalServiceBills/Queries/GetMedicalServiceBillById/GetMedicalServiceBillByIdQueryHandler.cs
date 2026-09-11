using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.MedicalServiceBills.Dtos;
using HSMBusiness.Features.MedicalServiceBills.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.MedicalServiceBills.Queries.GetMedicalServiceBillById
{
    public class GetMedicalServiceBillByIdQueryHandler(
        IValidator<GetMedicalServiceBillByIdQuery> validator,
        MedicalServiceBillRepository medicalServiceBillRepository)
    {
        public async Task<Result<MedicalServiceBillDto>> ExecuteAsync(GetMedicalServiceBillByIdQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var medicalServiceBill = await medicalServiceBillRepository.GetByIDAsync(query.ServiceId, query.BillId);
            if (medicalServiceBill is null)
            {
                return MedicalServiceBillErrors.NotFound;
            }

            return medicalServiceBill.ToDto();
        }
    }
}
