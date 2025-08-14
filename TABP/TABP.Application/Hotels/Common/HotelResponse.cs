using TABP.Application.Reviews.Common;
namespace TABP.Application.Hotels.Common
{
    public record HotelResponse(
        long Id,
        string Name,
        byte StarRating,
        string? Description,
        string BriefDescription,
        IEnumerable<ReviewResponse> Reviews
        );
}