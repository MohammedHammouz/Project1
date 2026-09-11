using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.MedicalServiceBills.Commands.DeleteMedicalServiceBill
{
    public class DeleteMedicalServiceBillCommandHandler(
        IValidator<DeleteMedicalServiceBillCommand> validator,
        MedicalServiceBillRepository medicalServiceBillRepository)
    {
        public async Task<Result<Deleted>> ExecuteAsync(DeleteMedicalServiceBillCommand command)
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

            var isDeleted = await medicalServiceBillRepository.DeleteAsync(medicalServiceBill);
            if (!isDeleted)
            {
                return Error.Failure("MedicalServiceBill_DeleteFailed", "Failed to delete bill service.");
            }

            return Result.Deleted;
        }
    }
}
