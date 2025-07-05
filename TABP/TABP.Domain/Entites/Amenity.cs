using TABP.Domain.Common;
namespace TABP.Domain.Entites
{
   public class Amenity: SoftDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }= null!;
        public string Description { get; set; } = null!;
        public ICollection<RoomClass> RoomClasses { get; set; } = [];
    }
}