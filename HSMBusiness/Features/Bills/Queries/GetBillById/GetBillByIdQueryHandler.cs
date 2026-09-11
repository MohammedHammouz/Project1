using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Bills.Dtos;
using HSMBusiness.Features.Bills.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Bills.Queries.GetBillById
{
    public class GetBillByIdQueryHandler(
        IValidator<GetBillByIdQuery> validator,
        BillRepository billRepository)
    {
        public async Task<Result<BillDto>> ExecuteAsync(GetBillByIdQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var bill = await billRepository.GetByIDAsync(query.Id);
            if (bill is null)
            {
                return BillErrors.NotFound;
            }

            return bill.ToDto();
        }
    }
}
