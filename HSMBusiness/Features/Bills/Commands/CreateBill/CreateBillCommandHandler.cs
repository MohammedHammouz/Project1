using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Bills.Dtos;
using HSMBusiness.Features.Bills.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Bills.Commands.CreateBill
{
    public class CreateBillCommandHandler(
        IValidator<CreateBillCommand> validator,
        BillRepository billRepository,
        PatientRepository patientRepository)
    {
        public async Task<Result<BillDto>> ExecuteAsync(CreateBillCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var patient = await patientRepository.GetByIDAsync(command.PatientId);
            if (patient is null)
            {
                return BillErrors.PatientNotFound;
            }

            var bill = new Bill
            {
                PatientID = command.PatientId,
                Status = command.Status,
                Date = command.Date,
                PartialPaymentAmount = command.PartialPaymentAmount,
                DueDate = command.DueDate,
                Invoice = command.Invoice,
                GrossAmount = command.GrossAmount,
                Discount = command.Discount,
                InsuranceCoverage = command.InsuranceCoverage,
                PatientResponsibility = command.PatientResponsibility
            };

            var created = await billRepository.AddAsync(bill);

            return created.ToDto();
        }
    }
}
