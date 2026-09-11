using HSMBusiness.Features.Reports.Dtos;
using HSMBusiness.Features.Reports.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Reports.Queries.GetReports
{
    public class GetReportsQueryHandler(ReportRepository reportRepository)
    {
        public async Task<Result<List<ReportDto>>> ExecuteAsync(GetReportsQuery query)
        {
            var reports = await reportRepository.GetAllAsync();

            return reports.ToDtos();
        }
    }
}
