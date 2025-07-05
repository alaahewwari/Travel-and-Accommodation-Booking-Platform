using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entites;
using TABP.Persistence.Constants;
namespace TABP.Persistence.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.ToTable(TableNames.Hotels);
            builder.HasKey(h => h.Id);
            builder.Property(h => h.Name)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.Property(h => h.Description)
                   .HasMaxLength(1000);
            builder.Property(h => h.BriefDescription)
                   .HasMaxLength(255);
            builder.Property(h => h.Address)
                   .IsRequired()
                   .HasMaxLength(500);
            builder.Property(h => h.StarRating)
                   .IsRequired()
                   .HasDefaultValue(0);
            builder.Property(h => h.LocationLatitude)
                   .IsRequired();
            builder.Property(h => h.LocationLongitude)
                   .IsRequired();
            builder.Property(h => h.CreatedAt)
                   .IsRequired();
            builder.HasOne(h => h.City)
                  .WithMany(c => c.Hotels)
                  .HasForeignKey(ForeignKeys.CityId);
            builder.HasOne(h => h.Owner)
                   .WithMany(o => o.Hotels)
                   .HasForeignKey(ForeignKeys.OwnerId);
            builder.HasMany(h => h.RoomClasses)
                   .WithOne()
                   .HasForeignKey(ForeignKeys.HotelId);
            builder.HasMany(h => h.HotelImages)
                   .WithOne(i => i.Hotel)
                   .HasForeignKey(ForeignKeys.HotelId);
            builder.HasMany(h => h.Reviews)
                   .WithOne(r => r.Hotel)
                   .HasForeignKey(ForeignKeys.HotelId);
            builder.HasQueryFilter(h => !h.IsDeleted);
        }
    }
}