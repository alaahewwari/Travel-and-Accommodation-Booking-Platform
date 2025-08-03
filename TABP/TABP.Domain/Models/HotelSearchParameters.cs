namespace TABP.Domain.Models
{
    public record HotelSearchParameters(
     DateTime CheckInDate,
     DateTime CheckOutDate,
     int Adults,
     int Children,
     int Rooms
 );
}