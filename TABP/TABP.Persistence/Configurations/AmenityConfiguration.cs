using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;
using TABP.Domain.Constants;
namespace TABP.Persistence.Configurations
{
    public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            builder.ToTable(TableNames.Amenities);
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Description).IsRequired().HasMaxLength(500);
            builder.HasMany(a => a.RoomClasses)
                .WithMany(rc => rc.Amenities)
                .UsingEntity<Dictionary<string, object>>(
                    TableNames.RoomClassAmenities,
                    right => right
                    .HasOne<RoomClass>()
                    .WithMany()
                    .HasForeignKey("RoomClassId")
                    .OnDelete(DeleteBehavior.Cascade),
                    left => left
                    .HasOne<Amenity>()
                    .WithMany()
                    .HasForeignKey("AmenityId")
                    .OnDelete(DeleteBehavior.Cascade)
                    );
           builder.HasQueryFilter(a => !a.IsDeleted);
        }
    }
}