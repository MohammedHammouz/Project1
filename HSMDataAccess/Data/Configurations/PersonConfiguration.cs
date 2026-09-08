using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSMDataAccess.Data.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(e => e.ID);
            builder.ToTable(
               t =>
               {
                   t
                   .HasCheckConstraint
                   ("CK_People_Email", "([Email] like '%_@_%._%')");
               });
            builder.ToTable(
              t =>
              {
                  t
                  .HasCheckConstraint
                  ("CK_People_Gender", "([Gender]='Female' OR [Gender]='Male')");
              });
            builder.ToTable(
              t =>
              {
                  t
                  .HasCheckConstraint
                  ("CK_People_Name", "(len(Trim([Name]))>=(2))");
              });
            builder.Property(e => e.ID)
             .HasColumnType("nchar(10)")
            .HasMaxLength(10)
            .HasDefaultValueSql("(N'(left(CONVERT([nvarchar](36),newid()),(10)))')")
            .ValueGeneratedOnAdd();
            builder.Property(e => e.Name)
            .HasColumnType("nvarchar(200)")
            .HasMaxLength(200);
            builder.Property(e => e.ContactNumber)
            .HasColumnType("nchar(10)")
            .HasMaxLength(10);
            builder.Property(e => e.Email)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);
            builder.Property(e => e.Gender)
            .HasColumnType("nchar(10)")
            .HasMaxLength(10);
            builder.Property(e => e.Address)
           .HasColumnType("text");
            builder.Property(e => e.DateOfBirth)
          .HasColumnType("date");
        }
    }
}