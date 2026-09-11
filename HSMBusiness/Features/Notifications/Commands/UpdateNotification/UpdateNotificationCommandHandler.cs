using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Notifications.Dtos;
using HSMBusiness.Features.Notifications.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Notifications.Commands.UpdateNotification
{
    public class UpdateNotificationCommandHandler(
        IValidator<UpdateNotificationCommand> validator,
        NotifictionRepository notificationRepository,
        PatientRepository patientRepository,
        UserRepository userRepository)
    {
        public async Task<Result<NotificationDto>> ExecuteAsync(UpdateNotificationCommand command)
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

            var patient = await patientRepository.GetByIDAsync(command.PatientId);
            if (patient is null)
            {
                return NotificationErrors.PatientNotFound;
            }

            var user = await userRepository.GetByIDAsync(command.UserId);
            if (user is null)
            {
                return NotificationErrors.UserNotFound;
            }

            notification.PatientID = command.PatientId;
            notification.UserID = command.UserId;
            notification.Type = command.Type;
            notification.Message = command.Message;
            notification.Status = command.Status;
            notification.SentOn = command.SentOn;
            notification.DeliveryConfirmation = command.DeliveryConfirmation;

            await notificationRepository.UpdateAsync(notification);

            return notification.ToDto();
        }
    }
}
