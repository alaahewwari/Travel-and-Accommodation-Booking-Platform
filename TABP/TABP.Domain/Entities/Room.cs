using TABP.Domain.Entities.Common;
namespace TABP.Domain.Entities
{
    public class Room: SoftDeletable
    {
        public long Id { get; set; }
        public string Number { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long RoomClassId { get; set; }
        public long HotelId { get; set; }
        public RoomClass RoomClass { get; set; } = null!;
        public Hotel Hotel { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = [];
    }
}