using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSMDataAccess.Data.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(e => e.ID);

                builder.Property(e => e.ID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                builder.Property(e => e.PatientID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                builder.Property(e => e.DoctorID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                builder.Property(e => e.Date)
                    .HasColumnType("date")
                    .IsRequired();

                builder.Property(e => e.Time)
                    .HasColumnType("time(7)");

                builder.Property(e => e.Duration)
                    .HasColumnType("int")
                    .IsRequired();

                builder.Property(e => e.Status)
                    .HasColumnType("nchar(15)")
                    .IsRequired();

                builder.Property(e => e.NotificationSent)
                    .HasColumnType("bit");
                builder.ToTable(
                 t =>
                 {
                     t
                     .HasCheckConstraint
                     ("CK_Appointments_Status", "([Status]='Cancelled' OR [Status]='Rescheduled' OR [Status]='Scheduled')");
                 });
                builder.HasOne(d => d.patient)
                 .WithMany(n => n.appointment)
                  .HasForeignKey(d => d.PatientID)
                  .OnDelete(DeleteBehavior.Restrict);
                builder.HasOne(d => d.doctor)
                 .WithMany(n => n.appointment)
                  .HasForeignKey(d => d.DoctorID)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
