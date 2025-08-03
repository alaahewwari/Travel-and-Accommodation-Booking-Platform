using TABP.Domain.Enums;
namespace TABP.Domain.Common
{
    public abstract class BaseImage
    {
        public long Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public ImageType ImageType { get; set; }
    }
}