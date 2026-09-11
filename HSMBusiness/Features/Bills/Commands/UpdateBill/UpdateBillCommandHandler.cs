using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Bills.Dtos;
using HSMBusiness.Features.Bills.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Bills.Commands.UpdateBill
{
    public class UpdateBillCommandHandler(
        IValidator<UpdateBillCommand> validator,
        BillRepository billRepository,
        PatientRepository patientRepository)
    {
        public async Task<Result<BillDto>> ExecuteAsync(UpdateBillCommand command)
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

            var patient = await patientRepository.GetByIDAsync(command.PatientId);
            if (patient is null)
            {
                return BillErrors.PatientNotFound;
            }

            bill.PatientID = command.PatientId;
            bill.Status = command.Status;
            bill.Date = command.Date;
            bill.PartialPaymentAmount = command.PartialPaymentAmount;
            bill.DueDate = command.DueDate;
            bill.Invoice = command.Invoice;
            bill.GrossAmount = command.GrossAmount;
            bill.Discount = command.Discount;
            bill.InsuranceCoverage = command.InsuranceCoverage;
            bill.PatientResponsibility = command.PatientResponsibility;

            await billRepository.UpdateAsync(bill);

            return bill.ToDto();
        }
    }
}
