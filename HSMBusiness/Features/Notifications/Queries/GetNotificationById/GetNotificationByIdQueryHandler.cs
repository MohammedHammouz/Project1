using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Notifications.Dtos;
using HSMBusiness.Features.Notifications.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Notifications.Queries.GetNotificationById
{
    public class GetNotificationByIdQueryHandler(
        IValidator<GetNotificationByIdQuery> validator,
        NotifictionRepository notificationRepository)
    {
        public async Task<Result<NotificationDto>> ExecuteAsync(GetNotificationByIdQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var notification = await notificationRepository.GetByIDAsync(query.Id);
            if (notification is null)
            {
                return NotificationErrors.NotFound;
            }

            return notification.ToDto();
        }
    }
}
