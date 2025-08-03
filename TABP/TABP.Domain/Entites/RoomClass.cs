using TABP.Domain.Enums;
namespace TABP.Domain.Entites
{
    public class RoomClass
    {
        public long Id { get; set; }
        public RoomType Type { get; set; }
        public string? Description { get; set; }
        public decimal PricePerNight { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public Discount? Discount { get; set; }
        public ICollection<RoomImage> RoomImages { get; set; } = [];
        public ICollection<Amenity> Amenities { get; set; } = [];
        public ICollection<Room> Rooms { get; set; } = [];
    }
}