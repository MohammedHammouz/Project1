namespace HSMBusiness.Features.Appointments.Dtos
{
    public class AppointmentDto
    {
        public string Id { get; set; } = string.Empty;
        public string PatientId { get; set; } = string.Empty;
        public string DoctorId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public TimeSpan? Time { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool? NotificationSent { get; set; }
    }
}
