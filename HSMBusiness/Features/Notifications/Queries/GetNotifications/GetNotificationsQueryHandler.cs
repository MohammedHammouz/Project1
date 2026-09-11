using HSMBusiness.Features.Notifications.Dtos;
using HSMBusiness.Features.Notifications.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Notifications.Queries.GetNotifications
{
    public class GetNotificationsQueryHandler(NotifictionRepository notificationRepository)
    {
        public async Task<Result<List<NotificationDto>>> ExecuteAsync(GetNotificationsQuery query)
        {
            var notifications = await notificationRepository.GetAllAsync();

            return notifications.ToDtos();
        }
    }
}
