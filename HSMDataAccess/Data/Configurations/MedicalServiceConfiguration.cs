using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSMDataAccess.Data.Configurations
{
    public class MedicalServiceConfiguration : IEntityTypeConfiguration<MedicalService>
    {
        public void Configure(EntityTypeBuilder<MedicalService> builder)
        {
            builder.HasKey(x => x.ID);

                builder.Property(x => x.ID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                builder.Property(x => x.ServiceName)
                    .HasColumnType("varchar(255)")
                    .IsRequired();

                builder.Property(x => x.CategoryID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                builder.Property(x => x.DefaultPrice)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                builder.Property(x => x.Description)
                    .HasColumnType("text");

                builder.Property(x => x.DurationMinutes);

                builder.HasIndex(e => e.ServiceName, "UQ__MedicalService__ServiceName");
                builder.HasOne(d => d.serviceCategory)
                .WithMany(n => n.medicalService)
                 .HasForeignKey(d => d.CategoryID)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
