using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSMDataAccess.Data.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");

                builder.HasKey(x => x.ID);

                builder.Property(x => x.ID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                builder.Property(x => x.UserID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                builder.Property(x => x.Entity)
                    .HasColumnType("nvarchar(50)")
                    .IsRequired();

                builder.Property(x => x.Action)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                builder.Property(x => x.Timestamp)
                    .HasColumnType("datetime")
                    .IsRequired();

                builder.Property(x => x.Details)
                    .HasColumnType("text");
                builder.ToTable(
                 t =>
                 {
                     t
                     .HasCheckConstraint
                     ("CK_AuditLogs_Action", "([Action]='Access' OR [Action]='Delete' OR [Action]='Update' OR [Action]='Create')");
                 });
                builder.HasOne(d => d.user)
                 .WithMany(n => n.auditLog)
                  .HasForeignKey(d => d.UserID)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
