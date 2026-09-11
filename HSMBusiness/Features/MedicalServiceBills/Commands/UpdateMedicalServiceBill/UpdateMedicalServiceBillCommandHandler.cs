using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.MedicalServiceBills.Dtos;
using HSMBusiness.Features.MedicalServiceBills.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.MedicalServiceBills.Commands.UpdateMedicalServiceBill
{
    public class UpdateMedicalServiceBillCommandHandler(
        IValidator<UpdateMedicalServiceBillCommand> validator,
        MedicalServiceBillRepository medicalServiceBillRepository)
    {
        public async Task<Result<MedicalServiceBillDto>> ExecuteAsync(UpdateMedicalServiceBillCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var medicalServiceBill = await medicalServiceBillRepository.GetByIDAsync(command.ServiceId, command.BillId);
            if (medicalServiceBill is null)
            {
                return MedicalServiceBillErrors.NotFound;
            }

            medicalServiceBill.Quantity = command.Quantity;
            medicalServiceBill.UnitPrice = command.UnitPrice;
            medicalServiceBill.TotalPrice = command.TotalPrice;

            await medicalServiceBillRepository.UpdateAsync(medicalServiceBill);

            return medicalServiceBill.ToDto();
        }
    }
}
