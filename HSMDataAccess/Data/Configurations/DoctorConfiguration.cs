using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSMDataAccess.Data.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(e => e.ID);
                builder.Property(e => e.ID)
                 .HasColumnType("nchar(10)")
                .HasMaxLength(10)
                .HasDefaultValueSql("(N'(left(CONVERT([nvarchar](36),newid()),(10)))')")
                .ValueGeneratedOnAdd();
                
                builder.HasIndex(e => e.DepartmentID, "IX_Doctors_Departments");
                builder.HasIndex(e => e.UserID, "IX_Doctors_Users");
                builder.ToTable(
                    t =>
                    {
                        t
                        .HasCheckConstraint
                        ("CK_Doctors_Status", "([Status]=0 OR [Status]=1)");
                    });
                builder.Property(e => e.Specialization)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();
                builder.Property(e => e.Status)
                .HasColumnType("bit")
                .IsRequired();
                builder.HasOne(d => d.Department)
                .WithOne()
                 .HasForeignKey<Doctor>(d => d.DepartmentID)
                 .OnDelete(DeleteBehavior.Restrict);
                builder.HasOne(d => d.User)
                .WithOne()
                 .HasForeignKey<Doctor>(d => d.UserID);
        }
    }
}
