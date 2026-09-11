using HSMBusiness.Features.Appointments.Dtos;
using HSMBusiness.Features.Appointments.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Appointments.Queries.GetAppointments
{
    public class GetAppointmentsQueryHandler(AppointmentRepository appointmentRepository)
    {
        public async Task<Result<List<AppointmentDto>>> ExecuteAsync(GetAppointmentsQuery query)
        {
            var appointments = await appointmentRepository.GetAllAsync();

            return appointments.ToDtos();
        }
    }
}
