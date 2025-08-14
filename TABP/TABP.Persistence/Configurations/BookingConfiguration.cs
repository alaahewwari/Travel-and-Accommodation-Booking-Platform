using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TABP.Domain.Entities;
using TABP.Domain.Constants;
namespace TABP.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable(TableNames.Bookings);
            builder.HasKey(b => b.Id);
            builder.Property(b => b.TotalPrice)
                   .IsRequired()
                   .HasColumnType("decimal(10,2)");
            builder.Property(b => b.CheckInDate)
                   .IsRequired();
            builder.Property(b => b.CheckOutDate)
                   .IsRequired();
            builder.Property(b => b.CreatedAt)
                   .IsRequired();
            builder.Property(b => b.UpdatedAt)
                   .IsRequired();
            builder.HasMany(b => b.Rooms)
                   .WithMany(r => r.Bookings);
            builder.HasOne(b => b.User)
                   .WithMany(u => u.Bookings)
                   .HasForeignKey(b => b.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(b => !b.IsDeleted);
        }
    }
}