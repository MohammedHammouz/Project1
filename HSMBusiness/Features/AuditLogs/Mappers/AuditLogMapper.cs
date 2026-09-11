using HSMBusiness.Features.AuditLogs.Dtos;
using HSMDataAccess.Entities;

namespace HSMBusiness.Features.AuditLogs.Mappers
{
    public static class AuditLogMapper
    {
        public static AuditLogDto ToDto(this AuditLog entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new AuditLogDto
            {
                Id = entity.ID,
                UserId = entity.UserID,
                Entity = entity.Entity,
                Action = entity.Action,
                Timestamp = entity.Timestamp,
                Details = entity.Details
            };
        }

        public static List<AuditLogDto> ToDtos(this IEnumerable<AuditLog> entities)
            => entities.Select(e => e.ToDto()).ToList();
    }
}
