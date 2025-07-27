using TABP.Domain.Entities;
namespace TABP.Domain.Models.Hotel
{
    public record HotelSearchResultResponse(
     long Id,
     string Name,
     int StarRating,
     IEnumerable<Review> Reviews,
     string? BriefDescription,
     string? ThumbnailUrl,
     decimal PricePerNightStartFrom
 );
}