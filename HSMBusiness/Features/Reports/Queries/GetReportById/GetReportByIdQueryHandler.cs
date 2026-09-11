using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Reports.Dtos;
using HSMBusiness.Features.Reports.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Reports.Queries.GetReportById
{
    public class GetReportByIdQueryHandler(
        IValidator<GetReportByIdQuery> validator,
        ReportRepository reportRepository)
    {
        public async Task<Result<ReportDto>> ExecuteAsync(GetReportByIdQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var report = await reportRepository.GetByIDAsync(query.Id);
            if (report is null)
            {
                return ReportErrors.NotFound;
            }

            return report.ToDto();
        }
    }
}
