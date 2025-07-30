using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities.Common;
namespace TABP.Persistence.Common
{
    public abstract class BaseImageConfiguration<TImage> : IEntityTypeConfiguration<TImage>
    where TImage : BaseImage
    {
        public virtual void Configure(EntityTypeBuilder<TImage> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.ImageUrl)
                   .IsRequired()
                   .HasMaxLength(2048);
            builder.Property(i => i.ImageType)
                   .IsRequired()
                   .HasConversion<int>();
        }
    }
}