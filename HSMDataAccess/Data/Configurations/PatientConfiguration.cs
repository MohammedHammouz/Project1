using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSMDataAccess.Data.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(e => e.ID);
                builder.ToTable(
                   t =>
                   {
                       t
                       .HasCheckConstraint
                       ("CK_Patients_Status", "([Status]=(1) OR [Status]=(0))");
                   });

                builder.Property(e => e.ID)
                .ValueGeneratedOnAdd();
                builder.Property(e => e.ID)
                .HasColumnType("nchar(10)")
                .HasMaxLength(10);
                builder.Property(e => e.MedicalHistory)
                .HasColumnType("text");
                builder.Property(e => e.Status)
                .HasColumnType("bit");
                builder.Property(e => e.PersonID)
                .HasColumnType("nchar(10)")
                .HasMaxLength(10);
                builder.HasOne(d => d.Person)
                 .WithOne(e => e.Patient)
                  .HasForeignKey<Patient>(d => d.PersonID)
                  .OnDelete(DeleteBehavior.Restrict)
                  ;
        }
    }
}
