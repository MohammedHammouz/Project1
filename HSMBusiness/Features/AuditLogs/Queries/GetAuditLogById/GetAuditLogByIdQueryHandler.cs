using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.AuditLogs.Dtos;
using HSMBusiness.Features.AuditLogs.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.AuditLogs.Queries.GetAuditLogById
{
    public class GetAuditLogByIdQueryHandler(
        IValidator<GetAuditLogByIdQuery> validator,
        AuditLogRepository auditLogRepository)
    {
        public async Task<Result<AuditLogDto>> ExecuteAsync(GetAuditLogByIdQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var auditLog = await auditLogRepository.GetByIDAsync(query.Id);
            if (auditLog is null)
            {
                return AuditLogErrors.NotFound;
            }

            return auditLog.ToDto();
        }
    }
}
