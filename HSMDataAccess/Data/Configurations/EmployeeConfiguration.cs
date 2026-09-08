using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSMDataAccess.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.ID);
                builder.Property(e => e.ID)
                .ValueGeneratedOnAdd();
                builder.Property(e => e.Salary)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();
                builder.Property(e => e.HireDate)
                .IsRequired();
                builder.Property(e => e.PersonID)
                .HasColumnType("nchar(10)")
                .HasMaxLength(10)
                .IsRequired();
                builder.Property(e => e.IsActive)
                .HasColumnType("bit")
                .IsRequired();
                builder.HasOne(d => d.Person)
                 .WithOne(e => e.Employee)
                  .HasForeignKey<Employee>(d => d.PersonID)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Employees_People");
        }
        
    }
}
