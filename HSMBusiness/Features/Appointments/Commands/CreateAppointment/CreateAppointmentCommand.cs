namespace HSMBusiness.Features.Appointments.Commands.CreateAppointment
{
    public sealed record CreateAppointmentCommand(
        string PatientId,
        string DoctorId,
        DateTime Date,
        TimeSpan? Time,
        int Duration,
        string Status,
        bool? NotificationSent);
}
