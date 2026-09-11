using HSMBusiness.Features.AuditLogs.Dtos;
using HSMBusiness.Features.AuditLogs.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.AuditLogs.Queries.GetAuditLogs
{
    public class GetAuditLogsQueryHandler(AuditLogRepository auditLogRepository)
    {
        public async Task<Result<List<AuditLogDto>>> ExecuteAsync(GetAuditLogsQuery query)
        {
            var auditLogs = await auditLogRepository.GetAllAsync();

            return auditLogs.ToDtos();
        }
    }
}
