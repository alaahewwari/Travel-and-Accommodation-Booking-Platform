namespace TABP.Application.Bookings.Common
{
    public class BookingCreationResponse
    {
        public long Id { get; set; }
        public string HotelName { get; set; } = null!;
        public IEnumerable<string> RoomNumbers { get; set; } = [];
        public decimal TotalPrice { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public PaymentInfo PaymentInfo { get; set; } = null!;
    }
}