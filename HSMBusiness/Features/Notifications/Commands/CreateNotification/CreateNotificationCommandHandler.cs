using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Notifications.Dtos;
using HSMBusiness.Features.Notifications.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Notifications.Commands.CreateNotification
{
    public class CreateNotificationCommandHandler(
        IValidator<CreateNotificationCommand> validator,
        NotifictionRepository notificationRepository,
        PatientRepository patientRepository,
        UserRepository userRepository)
    {
        public async Task<Result<NotificationDto>> ExecuteAsync(CreateNotificationCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
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

            var notification = new Notification
            {
                PatientID = command.PatientId,
                UserID = command.UserId,
                Type = command.Type,
                Message = command.Message,
                Status = command.Status,
                SentOn = command.SentOn,
                DeliveryConfirmation = command.DeliveryConfirmation
            };

            var created = await notificationRepository.AddAsync(notification);

            return created.ToDto();
        }
    }
}
