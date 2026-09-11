namespace HSMBusiness.Features.Appointments.Commands.UpdateAppointment
{
    public sealed record UpdateAppointmentCommand(
        string Id,
        string PatientId,
        string DoctorId,
        DateTime Date,
        TimeSpan? Time,
        int Duration,
        string Status,
        bool? NotificationSent);
}
