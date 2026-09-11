using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.AuditLogs.Commands.DeleteAuditLog
{
    public class DeleteAuditLogCommandHandler(
        IValidator<DeleteAuditLogCommand> validator,
        AuditLogRepository auditLogRepository)
    {
        public async Task<Result<Deleted>> ExecuteAsync(DeleteAuditLogCommand command)
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

            var isDeleted = await auditLogRepository.DeleteAsync(auditLog);
            if (!isDeleted)
            {
                return Error.Failure("AuditLog_DeleteFailed", "Failed to delete audit log.");
            }

            return Result.Deleted;
        }
    }
}
