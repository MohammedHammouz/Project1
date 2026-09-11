using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.AuditLogs.Dtos;
using HSMBusiness.Features.AuditLogs.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.AuditLogs.Commands.CreateAuditLog
{
    public class CreateAuditLogCommandHandler(
        IValidator<CreateAuditLogCommand> validator,
        AuditLogRepository auditLogRepository,
        UserRepository userRepository)
    {
        public async Task<Result<AuditLogDto>> ExecuteAsync(CreateAuditLogCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var user = await userRepository.GetByIDAsync(command.UserId);
            if (user is null)
            {
                return AuditLogErrors.UserNotFound;
            }

            var auditLog = new AuditLog
            {
                UserID = command.UserId,
                Entity = command.Entity,
                Action = command.Action,
                Timestamp = command.Timestamp,
                Details = command.Details
            };

            var created = await auditLogRepository.AddAsync(auditLog);

            return created.ToDto();
        }
    }
}
