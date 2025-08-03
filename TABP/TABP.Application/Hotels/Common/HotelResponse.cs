using TABP.Domain.Entites;

namespace TABP.Application.Hotels.Common
{
    public record HotelResponse(
        long Id,
        string Name,
        int starRating,
        string? Description,
        string BriefDescription
        //List<ReviewResponse>? Review
        );
}