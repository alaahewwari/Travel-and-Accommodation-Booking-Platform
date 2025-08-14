using TABP.Domain.Models.Owner;
namespace TABP.Domain.Models.Hotel
{
    public record HotelForManagement(
       long Id,
       string Name,
       int StarRating,
       OwnerForManagement Owner,
       int NumberOfRooms,
       DateTime CreatedAt,
       DateTime? ModifiedAt
   );
}