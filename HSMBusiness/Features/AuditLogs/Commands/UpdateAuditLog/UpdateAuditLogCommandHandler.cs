using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.AuditLogs.Dtos;
using HSMBusiness.Features.AuditLogs.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.AuditLogs.Commands.UpdateAuditLog
{
    public class UpdateAuditLogCommandHandler(
        IValidator<UpdateAuditLogCommand> validator,
        AuditLogRepository auditLogRepository,
        UserRepository userRepository)
    {
        public async Task<Result<AuditLogDto>> ExecuteAsync(UpdateAuditLogCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var auditLog = await auditLogRepository.GetByIDAsync(command.Id);
            if (auditLog is null)
            {
                return AuditLogErrors.NotFound;
            }

            var user = await userRepository.GetByIDAsync(command.UserId);
            if (user is null)
            {
                return AuditLogErrors.UserNotFound;
            }

            auditLog.UserID = command.UserId;
            auditLog.Entity = command.Entity;
            auditLog.Action = command.Action;
            auditLog.Timestamp = command.Timestamp;
            auditLog.Details = command.Details;

            await auditLogRepository.UpdateAsync(auditLog);

            return auditLog.ToDto();
        }
    }
}
