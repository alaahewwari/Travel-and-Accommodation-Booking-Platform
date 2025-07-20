using TABP.Domain.Common;
using TABP.Domain.Enums;
namespace TABP.Domain.Entities
{
    public class RoomClass : SoftDeletable
    {
        public long Id { get; set; }
        public RoomType Type { get; set; }
        public string? Description { get; set; }
        public string BriefDescription { get; set; } = null!;
        public decimal PricePerNight { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long HotelId { get; set; }
        public int? DiscountId { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public Discount? Discount { get; set; }
        public ICollection<RoomImage> RoomImages { get; set; } = [];
        public ICollection<Amenity> Amenities { get; set; } = [];
        public ICollection<Room> Rooms { get; set; } = [];
    }
}