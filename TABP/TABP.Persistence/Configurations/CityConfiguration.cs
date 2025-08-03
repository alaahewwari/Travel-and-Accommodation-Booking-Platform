using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;
using TABP.Domain.Constants;
namespace TABP.Persistence.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable(TableNames.Cities);
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(c => c.Country)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(c => c.PostOffice)
                   .IsRequired()
                   .HasMaxLength(5)
                   .IsFixedLength();
            builder.Property(c => c.CreatedAt)
                   .IsRequired();
            builder.Property(c => c.UpdatedAt)
                   .IsRequired();
            builder.HasMany(c => c.Hotels)
                   .WithOne(h => h.City)
                   .HasForeignKey(c => c.CityId);
            builder.HasMany(c => c.CityImages)
                   .WithOne(i => i.City)
                   .HasForeignKey(c => c.CityId);
            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}