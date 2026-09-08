using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSMDataAccess.Data.Configurations
{
    public class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
    {
        public void Configure(EntityTypeBuilder<ServiceCategory> builder)
        {
            builder.ToTable("ServiceCategories");

                builder.HasKey(x => x.ID);

                builder.Property(x => x.ID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                builder.Property(x => x.CategoryName)
                    .HasColumnType("varchar(100)")
                    .IsRequired();

                builder.Property(x => x.CategoryDescription)
                    .HasColumnType("text");
                builder.HasIndex(e => e.CategoryName)
                .IsUnique()
                .HasDatabaseName("UQ__Service__CategoryName");
        }
    }
}
