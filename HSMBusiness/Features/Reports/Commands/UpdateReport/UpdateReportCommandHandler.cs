using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Reports.Dtos;
using HSMBusiness.Features.Reports.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Reports.Commands.UpdateReport
{
    public class UpdateReportCommandHandler(
        IValidator<UpdateReportCommand> validator,
        ReportRepository reportRepository,
        UserRepository userRepository)
    {
        public async Task<Result<ReportDto>> ExecuteAsync(UpdateReportCommand command)
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

            var user = await userRepository.GetByIDAsync(command.GeneratedBy);
            if (user is null)
            {
                return ReportErrors.UserNotFound;
            }

            report.Type = command.Type;
            report.GeneratedOn = command.GeneratedOn;
            report.GeneratedBy = command.GeneratedBy;
            report.AppointmentCount = command.AppointmentCount;
            report.Revenue = command.Revenue;
            report.PaymentsReceived = command.PaymentsReceived;
            report.PendingPayments = command.PendingPayments;
            report.Metrics = command.Metrics;
            report.ExportFormat = command.ExportFormat;
            report.Status = command.Status;
            report.Notes = command.Notes;

            await reportRepository.UpdateAsync(report);

            return report.ToDto();
        }
    }
}
