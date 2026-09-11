using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Bills.Commands.DeleteBill
{
    public class DeleteBillCommandHandler(
        IValidator<DeleteBillCommand> validator,
        BillRepository billRepository)
    {
        public async Task<Result<Deleted>> ExecuteAsync(DeleteBillCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var bill = await billRepository.GetByIDAsync(command.Id);
            if (bill is null)
            {
                return BillErrors.NotFound;
            }

            var isDeleted = await billRepository.DeleteAsync(bill);
            if (!isDeleted)
            {
                return Error.Failure("Bill_DeleteFailed", "Failed to delete bill.");
            }

            return Result.Deleted;
        }
    }
}
