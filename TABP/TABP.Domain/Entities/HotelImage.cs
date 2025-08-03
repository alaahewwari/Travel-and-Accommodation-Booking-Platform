using TABP.Domain.Common;
namespace TABP.Domain.Entities
{
    public class HotelImage: BaseImage
    {
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
    }
}