using TABP.Domain.Entities.Common;
using TABP.Domain.Enums;
namespace TABP.Domain.Entities
{
    public class Booking : SoftDeletable
    {
        public long Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? GuestRemarks { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long UserId { get; set; }
        public long HotelId { get; set; }
        public ICollection<Room> Rooms { get; set; } = [];
        public User User { get; set; } = null!;
        public Invoice? Invoice { get; set; }
        public Hotel Hotel { get; set; } = null!;
    }
}