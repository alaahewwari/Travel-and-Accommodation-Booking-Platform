using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;
using TABP.Domain.Constants;
namespace TABP.Persistence.Configurations
{
    public class HotelImageConfiguration : BaseImageConfiguration<HotelImage>
    {
        public override void Configure(EntityTypeBuilder<HotelImage> builder)
        {
            base.Configure(builder);
            builder.HasOne(i => i.Hotel)
                   .WithMany(h => h.HotelImages)
                   .HasForeignKey(i => i.HotelId);
        }
    }
}