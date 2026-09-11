using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Reports.Commands.DeleteReport
{
    public class DeleteReportCommandHandler(
        IValidator<DeleteReportCommand> validator,
        ReportRepository reportRepository)
    {
        public async Task<Result<Deleted>> ExecuteAsync(DeleteReportCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var report = await reportRepository.GetByIDAsync(command.Id);
            if (report is null)
            {
                return ReportErrors.NotFound;
            }

            var isDeleted = await reportRepository.DeleteAsync(report);
            if (!isDeleted)
            {
                return Error.Failure("Report_DeleteFailed", "Failed to delete report.");
            }

            return Result.Deleted;
        }
    }
}
