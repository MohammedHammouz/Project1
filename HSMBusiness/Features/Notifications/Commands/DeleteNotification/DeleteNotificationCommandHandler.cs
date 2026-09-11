using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Notifications.Commands.DeleteNotification
{
    public class DeleteNotificationCommandHandler(
        IValidator<DeleteNotificationCommand> validator,
        NotifictionRepository notificationRepository)
    {
        public async Task<Result<Deleted>> ExecuteAsync(DeleteNotificationCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var notification = await notificationRepository.GetByIDAsync(command.Id);
            if (notification is null)
            {
                return NotificationErrors.NotFound;
            }

            var isDeleted = await notificationRepository.DeleteAsync(notification);
            if (!isDeleted)
            {
                return Error.Failure("Notification_DeleteFailed", "Failed to delete notification.");
            }

            return Result.Deleted;
        }
    }
}
