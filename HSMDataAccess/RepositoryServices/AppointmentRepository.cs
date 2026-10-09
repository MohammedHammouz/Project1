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
        public async Task<bool> DoctorHasAppointment(string DoctorID, DateOnly AppointmentDate,TimeOnly? time,string ID="")
        {
            var query = _context.Appointments
                .AnyAsync(d => d.DoctorID == DoctorID
                && d.Date == AppointmentDate 
                && d.Time==time
                 && d.ID != ID);
            return await query;
        }
        public async Task<bool> PatientHasAppointment(string PatientID, DateOnly AppointmentDate, TimeOnly? time,string ID="")
        {
            var query = _context.Appointments
                .AnyAsync(d => d.PatientID == PatientID 
                && d.Date == AppointmentDate
                && d.Time==time
                && d.ID!=ID);
            return await query;
        }
        public async Task<bool> CountCancelledAppointment(string PatientID,string DoctorID, DateOnly AppointmentDate, TimeOnly? time,string Status)
        {
            var query = _context.Appointments
                .AnyAsync(d =>d.DoctorID==DoctorID && d.PatientID == PatientID &&
                d.Date == AppointmentDate
                && d.Time == time && Status=="Cancelled");
            return await query;
        }
    }
}
