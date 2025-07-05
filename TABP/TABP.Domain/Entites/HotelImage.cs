using TABP.Domain.Common;
namespace TABP.Domain.Entites
{
    public class HotelImage: BaseImage
    {
        public Hotel Hotel { get; set; } = null!;
    }
}