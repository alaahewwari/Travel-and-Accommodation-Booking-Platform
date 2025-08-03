using TABP.Application.Owners.Common;
namespace TABP.Application.Hotels.Common
{
    public record HotelForManagementResponse(
        long Id,
        string Name,
        int StarRating,
        OwnerResponse Owner,
        int NumberOfRooms,
        DateTime CreatedAt,
        DateTime? ModifiedAt
        );
}