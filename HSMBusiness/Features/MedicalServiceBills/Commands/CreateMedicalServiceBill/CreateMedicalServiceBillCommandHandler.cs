using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.MedicalServiceBills.Dtos;
using HSMBusiness.Features.MedicalServiceBills.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.MedicalServiceBills.Commands.CreateMedicalServiceBill
{
    public class CreateMedicalServiceBillCommandHandler(
        IValidator<CreateMedicalServiceBillCommand> validator,
        MedicalServiceBillRepository medicalServiceBillRepository,
        MedicalServiceRepository medicalServiceRepository,
        BillRepository billRepository)
    {
        public async Task<Result<MedicalServiceBillDto>> ExecuteAsync(CreateMedicalServiceBillCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var service = await medicalServiceRepository.GetByIDAsync(command.ServiceId);
            if (service is null)
            {
                return MedicalServiceBillErrors.ServiceNotFound;
            }

            var bill = await billRepository.GetByIDAsync(command.BillId);
            if (bill is null)
            {
                return MedicalServiceBillErrors.BillNotFound;
            }

            var medicalServiceBill = new MedicalServiceBill
            {
                ServiceID = command.ServiceId,
                BillID = command.BillId,
                Quantity = command.Quantity,
                UnitPrice = command.UnitPrice,
                TotalPrice = command.TotalPrice
            };

            var created = await medicalServiceBillRepository.AddAsync(medicalServiceBill);

            return created.ToDto();
        }
    }
}
