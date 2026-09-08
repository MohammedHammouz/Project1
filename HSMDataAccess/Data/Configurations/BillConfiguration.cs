using HSMDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSMDataAccess.Data.Configurations
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
             builder.HasKey(e => e.ID);

                builder.Property(e => e.ID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                builder.Property(e => e.PatientID)
                    .HasColumnType("nchar(10)")
                    .IsRequired();

                builder.Property(e => e.Status)
                    .HasColumnType("varchar(30)")
                    .IsRequired();

                builder.Property(e => e.Date)
                    .HasColumnType("datetime");

                builder.Property(e => e.PartialPaymentAmount)
                    .HasColumnType("decimal(10,2)");

                builder.Property(e => e.DueDate)
                    .HasColumnType("datetime");

                builder.Property(e => e.Invoice)
                    .HasColumnType("varchar(50)")
                    .IsRequired();

                builder.Property(e => e.GrossAmount)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                builder.Property(e => e.Discount)
                    .HasColumnType("decimal(5,2)");

                builder.Property(e => e.InsuranceCoverage)
                    .HasColumnType("decimal(5,2)");

                builder.Property(e => e.PatientResponsibility)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();
                builder.HasIndex(e => e.Date, "idx_bills_date");
                builder.HasIndex(e => e.DueDate, "idx_bills_due_date");
                builder.HasIndex(e => e.PatientID, "idx_bills_patient");
                builder.HasIndex(e => e.Status, "idx_bills_status");
                builder.HasIndex(e => e.Invoice)
                .IsUnique()
                .HasDatabaseName("UQ_Bills_Invoice");
                builder.ToTable(
                 t =>
                 {
                     t
                     .HasCheckConstraint
                     ("CK_Bills_Status", "([Status]='Pending Insurance' OR [Status]='Cancelled' OR [Status]='Partially Paid' OR [Status]='Unpaid' OR [Status]='Paid')");
                 });
                builder.HasOne(d => d.patient)
                 .WithMany(n => n.bill)
                  .HasForeignKey(d => d.PatientID)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
