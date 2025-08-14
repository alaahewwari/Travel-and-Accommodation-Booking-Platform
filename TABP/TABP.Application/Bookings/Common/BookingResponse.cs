using TABP.Application.Rooms.Common;
namespace TABP.Application.Bookings.Common
{
    public record BookingResponse
    {
        public long Id { get; set; }
        public string HotelName { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<RoomResponse> Rooms { get; set; } = [];
    }
}