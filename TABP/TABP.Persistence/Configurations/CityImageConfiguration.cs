using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;
using TABP.Persistence.Common;
using TABP.Domain.Constants;
namespace TABP.Persistence.Configurations
{
    public class CityImageConfiguration : BaseImageConfiguration<CityImage>
    {
        public override void Configure(EntityTypeBuilder<CityImage> builder)
        {
            base.Configure(builder);
            builder.HasOne(ci => ci.City)
                   .WithMany(c => c.CityImages)
                   .HasForeignKey(ci => ci.CityId);
        }
    }
}