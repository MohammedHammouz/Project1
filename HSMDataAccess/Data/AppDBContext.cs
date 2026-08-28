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
        //public virtual DbSet<PatientEntity> Patients { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        //public virtual DbSet<ServicesCategoriesEntity> ServicesCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.ID);


                entity.ToTable(e =>
                            e.HasCheckConstraint
                            ("CK_Users_Role", "([Role]='Management' OR [Role]=' Accounts' OR [Role]='Nurse' OR [Role]='Doctor' OR [Role]='Admin')"));
                entity.ToTable(e =>
                            e.HasCheckConstraint
                            ("CK_Users_Status", "([Status]=(1) OR [Status]=(0))"));
                
                entity.Property(e => e.ID)
                .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                .HasColumnType("nchar(10)")
                .HasMaxLength(10)
                .IsRequired();
                entity.Property(e => e.Role)
                .IsRequired();
                entity.Property(e => e.Status)
                .HasColumnType("bit")
                .IsRequired();
                entity.Property(e => e.PasswordHash)
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255)
                .IsRequired();
                entity.Property(e => e.EmployeeID)
                .HasColumnType("nchar(10)")
                .HasMaxLength(10)
                .IsRequired();
                entity.HasOne(u => u.Employee)
                  .WithOne()
                  .HasForeignKey<User>(e => e.EmployeeID);
            });
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID)
                 .HasColumnType("nchar(10)")
                .HasMaxLength(10)
                .HasDefaultValueSql("(N'(left(CONVERT([nvarchar](36),newid()),(10)))')")
                .ValueGeneratedOnAdd();
                
                entity.HasIndex(e => e.DepartmentID, "IX_Doctors_Departments");
                entity.HasIndex(e => e.UserID, "IX_Doctors_Users");
                entity.ToTable(
                    t =>
                    {
                        t
                        .HasCheckConstraint
                        ("CK_Doctors_Status", "([Status]=0 OR [Status]=1)");
                    });
                entity.Property(e => e.Specialization)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();
                entity.Property(e => e.Status)
                .HasColumnType("bit")
                .IsRequired();
                entity.HasOne(d => d.Department)
                .WithOne()
                 .HasForeignKey<Doctor>(d => d.DepartmentID)
                 .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(d => d.User)
                .WithOne()
                 .HasForeignKey<Doctor>(d => d.UserID);
            });
            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.ID)
                 .HasColumnType("nchar(10)")
                .HasMaxLength(10)
                .HasDefaultValueSql("LEFT(CONVERT(nvarchar(36), NEWID()), 10)")
                .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                 .HasColumnType("nchar(10)")
                .HasMaxLength(10);
                entity.Property(e => e.HeadOf)
                 .HasColumnType("nchar(10)")
                .HasMaxLength(10);
            });
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.ToTable(
                   t =>
                   {
                       t
                       .HasCheckConstraint
                       ("CK_People_Email", "([Email] like '%_@_%._%')");
                   });
                entity.ToTable(
                  t =>
                  {
                      t
                      .HasCheckConstraint
                      ("CK_People_Gender", "([Gender]='Female' OR [Gender]='Male')");
                  });
                entity.ToTable(
                  t =>
                  {
                      t
                      .HasCheckConstraint
                      ("CK_People_Name", "(len(Trim([Name]))>=(2))");
                  });
                entity.Property(e => e.ID)
                 .HasColumnType("nchar(10)")
                .HasMaxLength(10)
                .HasDefaultValueSql("(N'(left(CONVERT([nvarchar](36),newid()),(10)))')")
                .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                .HasColumnType("nvarchar(200)")
                .HasMaxLength(200);
                entity.Property(e => e.ContactNumber)
                .HasColumnType("nchar(10)")
                .HasMaxLength(10);
                entity.Property(e => e.Email)
                .HasColumnType("nvarchar(250)")
                .HasMaxLength(250);
                entity.Property(e => e.Gender)
                .HasColumnType("nchar(10)")
                .HasMaxLength(10);
                entity.Property(e => e.Address)
               .HasColumnType("text");
                entity.Property(e => e.DateOfBirth)
              .HasColumnType("date");
            });
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.ToTable(
                   t =>
                   {
                       t
                       .HasCheckConstraint
                       ("CK_Patients_Status", "([Status]=(1) OR [Status]=(0))");
                   });

                entity.Property(e => e.ID)
                .ValueGeneratedOnAdd();
                entity.Property(e => e.ID)
                .HasColumnType("nchar(10)")
                .HasMaxLength(10);
                entity.Property(e => e.MedicalHistory)
                .HasColumnType("text");
                entity.Property(e => e.Status)
                .HasColumnType("bit");
                entity.Property(e => e.PersonID)
                .HasMaxLength(10);
                entity.HasOne(d => d.Person)
                 .WithOne(e => e.Patient)
                  .HasForeignKey<Patient>(d => d.PersonID)
                  .OnDelete(DeleteBehavior.Restrict)
                  ;
            });
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID)
                .ValueGeneratedOnAdd();
                entity.Property(e => e.Salary)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();
                entity.Property(e => e.HireDate)
                .IsRequired();
                entity.Property(e => e.PersonID)
                .HasColumnType("nchar(10)")
                .HasMaxLength(10)
                .IsRequired();
                entity.Property(e => e.IsActive)
                .HasColumnType("bit")
                .IsRequired();
                entity.HasOne(d => d.Person)
                 .WithOne(e => e.Employee)
                  .HasForeignKey<Employee>(d => d.PersonID)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Employees_People");
            });
            modelBuilder.Entity<Notifiction>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.ID)
                    .HasColumnType("nchar(10)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.PatientID)
                    .HasColumnType("nchar(10)")
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.UserID)
                    .HasColumnType("nchar(10)")
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.Type)
                    .HasColumnType("nchar(5)")
                    .HasMaxLength(5)
                    .IsRequired();

                entity.Property(e => e.Message)
                    .HasColumnType("text")
                    .IsRequired();

                entity.Property(e => e.Status)
                    .HasColumnType("nchar(10)")
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.SentOn)
                    .HasColumnType("datetime");

                entity.Property(e => e.DeliveryConfirmation)
                    .HasColumnType("bit");

                entity.HasOne(d => d.User)
                 .WithMany(n => n.notifiction)
                  .HasForeignKey(d => d.UserID)
                  .OnDelete(DeleteBehavior.Restrict);
                 
                entity.HasOne(d => d.patient)
                 .WithMany(n => n.notifiction)
                  .HasForeignKey(d => d.UserID)
                  .OnDelete(DeleteBehavior.Restrict);
                entity.ToTable(
                   t =>
                   {
                       t
                       .HasCheckConstraint
                       ("CK_Notification_Status", "([Status]='Pending' OR [Status]='Failed' OR [Status]='Sent')");
                   });
                entity.ToTable(
                   t =>
                   {
                       t
                       .HasCheckConstraint
                       ("CK_Notification_Type", "([Type]='SMS' OR [Type]='Email')");
                   });
            });
            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.ID)
                    .HasColumnType("nchar(10)")
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.Type)
                    .HasColumnType("nchar(10)")
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.GeneratedOn)
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(e => e.GeneratedBy)
                    .HasColumnType("nchar(10)")
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.AppointmentCount)
                    .HasColumnType("int");

                entity.Property(e => e.Revenue)
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PaymentsReceived)
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PendingPayments)
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Metrics)
                    .HasColumnType("text");

                entity.Property(e => e.ExportFormat)
                    .HasColumnType("nchar(5)")
                    .HasMaxLength(5);

                entity.Property(e => e.Status)
                    .HasColumnType("nchar(7)")
                    .HasMaxLength(7);

                entity.Property(e => e.Notes)
                    .HasColumnType("text");
                entity.ToTable(
                  t =>
                  {
                      t
                      .HasCheckConstraint
                      ("CK_Reports_ExportFormat", "([ExportFormat]='Excel' OR [ExportFormat]='PDF')");
                  });
                entity.ToTable(
                   t =>
                   {
                       t
                       .HasCheckConstraint
                       ("CK_Reports_Type", "([Type]='Monthly' OR [Type]='Weekly' OR [Type]='Daily')");
                   });
                entity.HasOne(d => d.user)
                .WithMany(n => n.report)
                 .HasForeignKey(d => d.GeneratedBy)
                 .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.ID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                entity.Property(e => e.PatientID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                entity.Property(e => e.DoctorID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(e => e.Time)
                    .HasColumnType("time(7)");

                entity.Property(e => e.Duration)
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(e => e.Status)
                    .HasColumnType("nchar(15)")
                    .IsRequired();

                entity.Property(e => e.NotificationSent)
                    .HasColumnType("bit");
                entity.ToTable(
                 t =>
                 {
                     t
                     .HasCheckConstraint
                     ("CK_Appointments_Status", "([Status]='Cancelled' OR [Status]='Rescheduled' OR [Status]='Scheduled')");
                 });
                entity.HasOne(d => d.patient)
                 .WithMany(n => n.appointment)
                  .HasForeignKey(d => d.PatientID)
                  .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(d => d.doctor)
                 .WithMany(n => n.appointment)
                  .HasForeignKey(d => d.DoctorID)
                  .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.ID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                entity.Property(e => e.PatientID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                entity.Property(e => e.Status)
                    .HasColumnType("varchar(30)")
                    .IsRequired();

                entity.Property(e => e.Date)
                    .HasColumnType("datetime");

                entity.Property(e => e.PartialPaymentAmount)
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.DueDate)
                    .HasColumnType("datetime");

                entity.Property(e => e.Invoice)
                    .HasColumnType("varchar(50)")
                    .IsRequired();

                entity.Property(e => e.GrossAmount)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.InsuranceCoverage)
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.PatientResponsibility)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();
                entity.HasIndex(e => e.Date, "idx_bills_date");
                entity.HasIndex(e => e.DueDate, "idx_bills_due_date");
                entity.HasIndex(e => e.PatientID, "idx_bills_patient");
                entity.HasIndex(e => e.Status, "idx_bills_status");
                entity.HasIndex(e => e.Invoice)
                .IsUnique()
                .HasDatabaseName("UQ_Bills_Invoice");
                entity.ToTable(
                 t =>
                 {
                     t
                     .HasCheckConstraint
                     ("CK_Bills_Status", "([Status]='Pending Insurance' OR [Status]='Cancelled' OR [Status]='Partially Paid' OR [Status]='Unpaid' OR [Status]='Paid')");
                 });
                entity.HasOne(d => d.patient)
                 .WithMany(n => n.bill)
                  .HasForeignKey(d => d.PatientID)
                  .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
