using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSMDataAccess.Data.Configurations
{
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.HasKey(x => x.ID);

                builder.Property(x => x.ID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                builder.Property(x => x.PatientID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                builder.Property(x => x.Diagnosis)
                    .HasColumnType("text")
                    .IsRequired(false);

                builder.Property(x => x.Treatment)
                    .HasColumnType("text")
                    .IsRequired(false);

                builder.Property(x => x.Prescriptions)
                    .HasColumnType("text")
                    .IsRequired(false);

                builder.Property(x => x.Status)
                    .HasColumnType("nchar(10)")
                    .IsRequired(false);

                builder.Property(x => x.AuditTrail)
                    .HasColumnType("text")
                    .IsRequired();

                builder.Property(x => x.AccessLevel)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                builder.HasOne(x => x.patient)
                    .WithMany(x => x.medicalRecord)
                    .HasForeignKey(x => x.PatientID)
                    .OnDelete(DeleteBehavior.Restrict);
                builder.ToTable(
                 t =>
                 {
                     t
                     .HasCheckConstraint
                     ("CK_MedicalRecords_Status", "([Status]='Archived' OR [Status]='Active')");
                 });
        }
    }
}
