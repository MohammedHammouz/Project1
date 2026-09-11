using HSMBusiness.Features.Appointments.Dtos;
using HSMDataAccess.Entities;

namespace HSMBusiness.Features.Appointments.Mappers
{
    public static class AppointmentMapper
    {
        public static AppointmentDto ToDto(this Appointment entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new AppointmentDto
            {
                Id = entity.ID,
                PatientId = entity.PatientID,
                DoctorId = entity.DoctorID,
                Date = entity.Date,
                Time = entity.Time,
                Duration = entity.Duration,
                Status = entity.Status,
                NotificationSent = entity.NotificationSent
            };
        }

        public static List<AppointmentDto> ToDtos(this IEnumerable<Appointment> entities)
            => entities.Select(e => e.ToDto()).ToList();
    }
}
