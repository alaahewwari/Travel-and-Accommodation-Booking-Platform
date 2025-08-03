using TABP.Domain.Common;
namespace TABP.Domain.Entites
{
    public class CityImage: BaseImage
    {
        public City City { get; set; } = null!;
    }
}