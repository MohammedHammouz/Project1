using HSMBusiness.Features.Bills.Dtos;
using HSMBusiness.Features.Bills.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Bills.Queries.GetBills
{
    public class GetBillsQueryHandler(BillRepository billRepository)
    {
        public async Task<Result<List<BillDto>>> ExecuteAsync(GetBillsQuery query)
        {
            var bills = await billRepository.GetAllAsync();

            return bills.ToDtos();
        }
    }
}
