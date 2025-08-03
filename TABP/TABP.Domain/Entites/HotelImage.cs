using TABP.Domain.Common;
namespace TABP.Domain.Entites
{
    public class HotelImage: BaseImage
    {
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
    }
}