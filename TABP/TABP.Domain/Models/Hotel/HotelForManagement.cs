using TABP.Domain.Entities;
namespace TABP.Domain.Models.Hotel
{
    public record HotelForManagement(
       long Id,
       string Name,
       int StarRating,
       Owner Owner,
       int NumberOfRooms,
       DateTime CreatedAt,
       DateTime? ModifiedAt
   );
}