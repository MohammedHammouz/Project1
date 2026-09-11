namespace HSMBusiness.Features.Notifications.Commands.CreateNotification
{
    public sealed record CreateNotificationCommand(
        string PatientId,
        string UserId,
        string Type,
        string Message,
        string Status,
        DateTime? SentOn,
        bool? DeliveryConfirmation);
}
