using TABP.Domain.Entities.Common;
namespace TABP.Domain.Entities
{
    public class Hotel : SoftDeletable
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string BriefDescription { get; set; } = null!;
        public string Address { get; set; } = null!;
        public byte StarRating { get; set; }
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CityId { get; set; }
        public long OwnerId { get; set; }
        public City City { get; set; } = null!;
        public Owner Owner { get; set; } = null!;
        public ICollection<RoomClass> RoomClasses { get; set; } = [];
        public ICollection<HotelImage> HotelImages { get; set; } = [];
        public ICollection<Review> Reviews { get; set; } = [];
        public ICollection<Room> Rooms { get; set; } = [];
        public ICollection<Booking> Bookings { get; set; } = [];
    }
}