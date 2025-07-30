namespace TABP.Domain.Exceptions
{
    public class InvalidBookingDatesException : BookingCreationException
    {
        public DateTime CheckInDate { get; }
        public DateTime CheckOutDate { get; }

        public InvalidBookingDatesException(DateTime checkInDate, DateTime checkOutDate)
            : base($"Invalid booking dates: Check-in date ({checkInDate:yyyy-MM-dd}) must be before check-out date ({checkOutDate:yyyy-MM-dd}).")
        {
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }
    }
}