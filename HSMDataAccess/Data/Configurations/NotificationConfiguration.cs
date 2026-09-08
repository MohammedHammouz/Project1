using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSMDataAccess.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(e => e.ID);

                builder.Property(e => e.ID)
                    .HasColumnType("nchar(10)")
                    .ValueGeneratedOnAdd();

                builder.Property(e => e.PatientID)
                    .HasColumnType("nchar(10)")
                    .HasMaxLength(10)
                    .IsRequired();

                builder.Property(e => e.UserID)
                    .HasColumnType("nchar(10)")
                    .HasMaxLength(10)
                    .IsRequired();

                builder.Property(e => e.Type)
                    .HasColumnType("nchar(5)")
                    .HasMaxLength(5)
                    .IsRequired();

                builder.Property(e => e.Message)
                    .HasColumnType("text")
                    .IsRequired();

                builder.Property(e => e.Status)
                    .HasColumnType("nchar(10)")
                    .HasMaxLength(10)
                    .IsRequired();

                builder.Property(e => e.SentOn)
                    .HasColumnType("datetime");

                builder.Property(e => e.DeliveryConfirmation)
                    .HasColumnType("bit");

                builder.HasOne(d => d.User)
                 .WithMany(n => n.notifiction)
                  .HasForeignKey(d => d.UserID)
                  .OnDelete(DeleteBehavior.Restrict);
                 
                builder.HasOne(d => d.patient)
                 .WithMany(n => n.notifiction)
                  .HasForeignKey(d => d.UserID)
                  .OnDelete(DeleteBehavior.Restrict);
                builder.ToTable(
                   t =>
                   {
                       t
                       .HasCheckConstraint
                       ("CK_Notification_Status", "([Status]='Pending' OR [Status]='Failed' OR [Status]='Sent')");
                   });
                builder.ToTable(
                   t =>
                   {
                       t
                       .HasCheckConstraint
                       ("CK_Notification_Type", "([Type]='SMS' OR [Type]='Email')");
                   });
        }
        
    }
}
