using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSMDataAccess.Data.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(e => e.ID);

                builder.Property(e => e.ID)
                    .HasColumnType("nchar(10)")
                    .HasMaxLength(10)
                    .IsRequired();

                builder.Property(e => e.Type)
                    .HasColumnType("nchar(10)")
                    .HasMaxLength(10)
                    .IsRequired();

                builder.Property(e => e.GeneratedOn)
                    .HasColumnType("datetime")
                    .IsRequired();

                builder.Property(e => e.GeneratedBy)
                    .HasColumnType("nchar(10)")
                    .HasMaxLength(10)
                    .IsRequired();

                builder.Property(e => e.AppointmentCount)
                    .HasColumnType("int");

                builder.Property(e => e.Revenue)
                    .HasColumnType("decimal(10, 2)");

                builder.Property(e => e.PaymentsReceived)
                    .HasColumnType("decimal(10, 2)");

                builder.Property(e => e.PendingPayments)
                    .HasColumnType("decimal(10, 2)");

                builder.Property(e => e.Metrics)
                    .HasColumnType("text");

                builder.Property(e => e.ExportFormat)
                    .HasColumnType("nchar(5)")
                    .HasMaxLength(5);

                builder.Property(e => e.Status)
                    .HasColumnType("nchar(7)")
                    .HasMaxLength(7);

                builder.Property(e => e.Notes)
                    .HasColumnType("text");
                builder.ToTable(
                  t =>
                  {
                      t
                      .HasCheckConstraint
                      ("CK_Reports_ExportFormat", "([ExportFormat]='Excel' OR [ExportFormat]='PDF')");
                  });
                builder.ToTable(
                   t =>
                   {
                       t
                       .HasCheckConstraint
                       ("CK_Reports_Type", "([Type]='Monthly' OR [Type]='Weekly' OR [Type]='Daily')");
                   });
                builder.HasOne(d => d.user)
                .WithMany(n => n.report)
                 .HasForeignKey(d => d.GeneratedBy)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
