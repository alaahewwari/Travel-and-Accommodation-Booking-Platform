using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;
using TABP.Persistence.Constants;
namespace TABP.Persistence.Configurations
{
    public class RoomClassConfiguration : IEntityTypeConfiguration<RoomClass>
    {
        public void Configure(EntityTypeBuilder<RoomClass> builder)
        {
            builder.ToTable(TableNames.RoomClasses);
            builder.HasKey(rc => rc.Id);
            builder.Property(rc => rc.Type).IsRequired();
            builder.Property(rc => rc.PricePerNight)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();
            builder.Property(rc => rc.AdultsCapacity).IsRequired();
            builder.Property(rc => rc.ChildrenCapacity).IsRequired();
            builder.Property(rc => rc.CreatedAt).IsRequired();
            builder.HasOne(rc => rc.Hotel)
                   .WithMany(h => h.RoomClasses)
                   .HasForeignKey(rc => rc.HotelId);
            builder.HasOne(rc => rc.Discount)
                   .WithMany()
                   .HasForeignKey(rc => rc.DiscountId)
                   .IsRequired(false);
        }
    }
}