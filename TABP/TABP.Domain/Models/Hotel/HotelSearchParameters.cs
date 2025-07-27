namespace TABP.Domain.Models.Hotel
{
    public record HotelSearchParameters(
     DateTime CheckInDate,
     DateTime CheckOutDate,
     int Adults,
     int Children,
     int Rooms
 );
}