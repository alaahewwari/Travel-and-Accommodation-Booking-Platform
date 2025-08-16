namespace TABP.API.Contracts.Bookings
{
    public class UpdateBookingRequest
    {
        public UpdateBookingRequest() { } //Required for morcatko library
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string? GuestRemarks { get; set; }
    }
}