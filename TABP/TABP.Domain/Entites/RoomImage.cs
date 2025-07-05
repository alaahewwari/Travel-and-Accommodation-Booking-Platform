using TABP.Domain.Common;
namespace TABP.Domain.Entites
{
    public class RoomImage: BaseImage
    {
        public RoomClass RoomClass { get; set; } = null!;
    }
}