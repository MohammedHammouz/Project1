namespace HSMBusiness.Features.Notifications.Dtos
{
    public class NotificationDto
    {
        public string Id { get; set; } = string.Empty;
        public string PatientId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime? SentOn { get; set; }
        public bool? DeliveryConfirmation { get; set; }
    }
}
