using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;
using TABP.Domain.Constants;
namespace TABP.Persistence.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable(TableNames.Invoices);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.InvoiceNumber).IsRequired();
            builder.Property(i => i.IssueDate).IsRequired();
            builder.Property(i => i.TotalAmount)
                    .HasPrecision(18, 2)
                   .IsRequired();
            builder.Property(i => i.Status)
                   .HasConversion<int>()
                   .IsRequired();
            builder.HasOne(i => i.Booking)
                   .WithOne(b => b.Invoice)
                   .HasForeignKey<Invoice>(i => i.BookingId);
            builder.HasIndex(i => i.BookingId)
                    .IsUnique();
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}