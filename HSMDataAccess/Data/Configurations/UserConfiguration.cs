using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSMDataAccess.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
                    
                builder.HasKey(e => e.ID);


                builder.ToTable(e =>
                            e.HasCheckConstraint
                            ("CK_Users_Role", "([Role]='Management' OR [Role]=' Accounts' OR [Role]='Nurse' OR [Role]='Doctor' OR [Role]='Admin')"));
                builder.ToTable(e =>
                            e.HasCheckConstraint
                            ("CK_Users_Status", "([Status]=(1) OR [Status]=(0))"));
                
                builder.Property(e => e.ID)
                .HasColumnType("nchar(10)")
                .HasMaxLength(10)
                .HasDefaultValueSql("(N'(left(CONVERT([nvarchar](36),newid()),(10)))')")
                .ValueGeneratedOnAdd();
                builder.Property(e => e.Name)
                .HasColumnType("nchar(10)")
                .HasMaxLength(10)
                .IsRequired();
                builder.Property(e => e.Role)
                .IsRequired();
                builder.Property(e => e.Status)
                .HasColumnType("bit")
                .IsRequired();
                builder.Property(e => e.PasswordHash)
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255)
                .IsRequired();
                builder.Property(e => e.EmployeeID)
                .HasColumnType("nchar(10)")
                .HasMaxLength(10)
                .IsRequired();
                builder.HasOne(u => u.Employee)
                  .WithOne()
                  .HasForeignKey<User>(e => e.EmployeeID);
        }
    }


}