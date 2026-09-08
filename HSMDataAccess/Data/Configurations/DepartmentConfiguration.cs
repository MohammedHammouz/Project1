using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSMDataAccess.Data.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(e => e.ID)
            .HasColumnType("nchar(10)")
            .HasMaxLength(10)
            .HasDefaultValueSql("LEFT(CONVERT(nvarchar(36), NEWID()), 10)")
            .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
            .HasColumnType("nchar(10)")
            .HasMaxLength(10);

            builder.Property(e => e.HeadOf)
            .HasColumnType("nchar(10)")
            .HasMaxLength(10);
        }
    }


}