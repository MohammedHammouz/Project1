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
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<DoctorEntity> Doctors { get; set; }
        public virtual DbSet<PersonEntity> People { get; set; }
        public virtual DbSet<PatientEntity> Patients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.UserEntity>(entity =>
            {
                entity.HasKey(e => e.UserID);
                
               
                entity.ToTable(e =>
                            e.HasCheckConstraint
                            ("CK_Users_Role", "([Role]='Management' OR [Role]=' Accounts' OR [Role]='Nurse' OR [Role]='Doctor' OR [Role]='Admin')"));
                entity.Property(e => e.UserID)
                .ValueGeneratedOnAdd();
                entity.Property(e => e.Role)
                .HasMaxLength(10)
                .HasColumnType("nvarchar(10)")
                .IsRequired();
                entity.Property(e => e.Status);
                entity.Property(e => e.HashPassword)
                .HasMaxLength(10)
                .HasColumnType("nchar(10)")
                .IsRequired();
                entity.HasOne(u => u.Employee)
                  .WithOne()
                  .HasForeignKey<UserEntity>(e => e.EmployeeID);
            });
            modelBuilder.Entity<DoctorEntity>(entity =>
            {
                entity.Property(e => e.ID)
                .ValueGeneratedOnAdd();
                entity.HasKey(e => e.ID);
                entity.HasIndex(e => e.DepartmentID, "IX_Doctors_Departments");
                entity.HasIndex(e => e.UserID, "IX_Doctors_Users");
                entity.ToTable(
                    t=> {
                        t
                        .HasCheckConstraint
                        ("CK_Doctors_Status", "([Status]='Inactive' OR [Status]='Active')");
                    });
                entity.Property(e => e.Specialization)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();
                entity.Property(e => e.Status)
                .HasColumnType("nchar(10)")
                .HasMaxLength(10)
                .IsRequired();
                entity.HasOne(d => d.Department)
                .WithOne()
                 .HasForeignKey<DoctorEntity>(d => d.DepartmentID)
                 .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(d => d.User)
                .WithOne()
                 .HasForeignKey<DoctorEntity>(d => d.UserID);
            });
            modelBuilder.Entity<PersonEntity>(entity =>
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
            modelBuilder.Entity<PatientEntity>(entity =>
            {
                entity.HasKey(e => e.PatientID);
                entity.ToTable(
                   t =>
                   {
                       t
                       .HasCheckConstraint
                       ("CK_Patients_Status", "([Status]=(1) OR [Status]=(0))");
                   });
                
                entity.Property(e => e.PatientID)
                .ValueGeneratedOnAdd();
                entity.Property(e => e.MedicalHistory)
                .HasColumnType("text");
                entity.Property(e => e.Status)
                .HasColumnType("bit");
                entity.Property(e => e.PersonID)
                .HasColumnType("nchar(10)")
                .HasMaxLength(10);
                entity.HasOne(d => d.Person)
                 .WithOne()
                  .HasForeignKey<PatientEntity>(d => d.PersonID)
                  .OnDelete(DeleteBehavior.Restrict)
                  ;
            });
        }
    }
}
