namespace TABP.Application.Reviews.Common
{
    /// <summary>
    /// Data Transfer Object representing a review returned by the API.
    /// </summary>
    public record ReviewResponse(
        long Id,
        int Rating,
        string? Comment,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        long UserId
        //long HotelId,
        //string HotelName
    );
}