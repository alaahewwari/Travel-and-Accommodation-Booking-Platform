using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entites;
using TABP.Persistence.Common;
using TABP.Persistence.Constants;
namespace TABP.Persistence.Configurations
{
    public class CityImageConfiguration : BaseImageConfiguration<CityImage>
    {
        public override void Configure(EntityTypeBuilder<CityImage> builder)
        {
            base.Configure(builder);
            builder.HasOne(i => i.City)
                   .WithMany(c => c.CityImages)
                   .HasForeignKey(ForeignKeys.CityId);
        }
    }
}