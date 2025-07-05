using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entites;
using TABP.Persistence.Constants;
namespace TABP.Persistence.Configurations
{
    public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            builder.ToTable(TableNames.Amenities);
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Description).IsRequired();
            builder.HasMany(a => a.RoomClasses)
                   .WithMany(rc => rc.Amenities)
                   .UsingEntity(j => j.ToTable(TableNames.RoomClassAmenities));
            builder.HasQueryFilter(a => !a.IsDeleted);
        }
    }
}