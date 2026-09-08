using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSMDataAccess.Data.Configurations
{
    public class BillServiceConfiguration : IEntityTypeConfiguration<BillService>
    {
        public void Configure(EntityTypeBuilder<BillService> builder)
        {
            builder.HasKey(x => new
                {
                    x.BillID,
                    x.ServiceID
                });

                builder.Property(x => x.BillID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                builder.Property(x => x.ServiceID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                builder.Property(x => x.Quantity)
                    .IsRequired(false);

                builder.Property(x => x.UnitPrice)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                builder.Property(x => x.TotalPrice)
                    .IsRequired(false);

                builder.HasOne(x => x.bill)
                    .WithMany(x => x.billService)
                    .HasForeignKey(x => x.BillID)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(x => x.medicalService)
                    .WithMany(x => x.billServices)
                    .HasForeignKey(x => x.ServiceID)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
