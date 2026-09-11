namespace HSMBusiness.Features.Notifications.Commands.UpdateNotification
{
    public sealed record UpdateNotificationCommand(
        string Id,
        string PatientId,
        string UserId,
        string Type,
        string Message,
        string Status,
        DateTime? SentOn,
        bool? DeliveryConfirmation);
}
