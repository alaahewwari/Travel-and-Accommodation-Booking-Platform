using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;
using TABP.Persistence.Common;
using TABP.Persistence.Constants;
namespace TABP.Persistence.Configurations
{
    public class CityImageConfiguration : BaseImageConfiguration<CityImage>
    {
        public override void Configure(EntityTypeBuilder<CityImage> builder)
        {
            base.Configure(builder);
            builder.HasOne(c => c.City)
                   .WithMany()
                   .HasForeignKey(c => c.CityId);
        }
    }
}