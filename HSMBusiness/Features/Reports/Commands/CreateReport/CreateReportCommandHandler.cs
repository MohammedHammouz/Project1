using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Reports.Dtos;
using HSMBusiness.Features.Reports.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Reports.Commands.CreateReport
{
    public class CreateReportCommandHandler(
        IValidator<CreateReportCommand> validator,
        ReportRepository reportRepository,
        UserRepository userRepository)
    {
        public async Task<Result<ReportDto>> ExecuteAsync(CreateReportCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var user = await userRepository.GetByIDAsync(command.GeneratedBy);
            if (user is null)
            {
                return ReportErrors.UserNotFound;
            }

            var report = new Report
            {
                Type = command.Type,
                GeneratedOn = command.GeneratedOn,
                GeneratedBy = command.GeneratedBy,
                AppointmentCount = command.AppointmentCount,
                Revenue = command.Revenue,
                PaymentsReceived = command.PaymentsReceived,
                PendingPayments = command.PendingPayments,
                Metrics = command.Metrics,
                ExportFormat = command.ExportFormat,
                Status = command.Status,
                Notes = command.Notes
            };

            var created = await reportRepository.AddAsync(report);

            return created.ToDto();
        }
    }
}
