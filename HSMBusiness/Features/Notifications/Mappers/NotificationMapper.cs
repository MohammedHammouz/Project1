using HSMBusiness.Features.Notifications.Dtos;
using HSMDataAccess.Entities;

namespace HSMBusiness.Features.Notifications.Mappers
{
    public static class NotificationMapper
    {
        public static NotificationDto ToDto(this Notification entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new NotificationDto
            {
                Id = entity.ID,
                PatientId = entity.PatientID,
                UserId = entity.UserID,
                Type = entity.Type,
                Message = entity.Message,
                Status = entity.Status,
                SentOn = entity.SentOn,
                DeliveryConfirmation = entity.DeliveryConfirmation
            };
        }

        public static List<NotificationDto> ToDtos(this IEnumerable<Notification> entities)
            => entities.Select(e => e.ToDto()).ToList();
    }
}
