using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Amount)
                .HasPrecision(18, 2);
            builder.HasOne(p => p.Booking)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(p => p.BookingId);
            builder.Property(p => p.PaymentIntentId)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(p => p.PaymentMethodId)
                .HasMaxLength(255);
            builder.Property(p => p.ClientSecret)
                .HasMaxLength(500);
            builder.Property(p => p.FailureReason)
                .HasMaxLength(1000);
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}