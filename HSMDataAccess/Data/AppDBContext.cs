using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HSMDataAccess.Data
{
    public class AppDBContext:DbContext
    {
        
        public AppDBContext(DbContextOptions<AppDBContext> options)
           : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Notification> Notifictions { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }
        public virtual DbSet<MedicalService> MedicalServices { get; set; }
        public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }
        public virtual DbSet<MedicalServiceBill> BillServices { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
        }
    }
}
