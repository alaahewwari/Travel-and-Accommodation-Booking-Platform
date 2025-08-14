using TABP.Domain.Entities.Common;
namespace TABP.Domain.Entities
{
    public class CityImage: BaseImage
    {
        public int CityId { get; set; }
        public City City { get; set; } = null!;
    }
}