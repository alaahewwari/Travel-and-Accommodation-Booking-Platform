using TABP.Domain.Entities.Common;
namespace TABP.Domain.Entities
{
    public class RoomImage: BaseImage
    {
        public long RoomClassId { get; set; }
        public RoomClass RoomClass { get; set; } = null!;
    }
}