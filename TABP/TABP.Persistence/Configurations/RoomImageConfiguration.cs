using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;
namespace TABP.Persistence.Configurations
{
    public class RoomImageConfiguration : BaseImageConfiguration<RoomImage>
    {
        public override void Configure(EntityTypeBuilder<RoomImage> builder)
        {
            base.Configure(builder);
            builder.HasOne(i => i.RoomClass)
                   .WithMany(r => r.RoomImages)
                   .HasForeignKey(ri => ri.RoomClassId);
        }
    }
}