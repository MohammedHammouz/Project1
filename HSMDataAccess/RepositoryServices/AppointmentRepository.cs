using HSMDataAccess.Data;
using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.RepositoryServices
{
    public class AppointmentRepository : GenericRepository<Appointment>
    {
        public AppointmentRepository(AppDBContext context) : base(context)
        {
        }
        public async Task<bool> DoctorHasAppointment(string DoctorID, DateOnly AppointmentDate,TimeOnly? time)
        {
            var query = _context.Appointments
                .AnyAsync(d => d.DoctorID == DoctorID &&
                d.Date == AppointmentDate && d.Time==time);
            return await query;
        }
        public async Task<bool> PatientHasAppointment(string PatientID, DateOnly AppointmentDate, TimeOnly? time)
        {
            var query = _context.Appointments
                .AnyAsync(d => d.PatientID == PatientID &&
                d.Date == AppointmentDate
                && d.Time==time);
            return await query;
        }
    }
}
