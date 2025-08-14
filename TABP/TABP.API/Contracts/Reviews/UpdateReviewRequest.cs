namespace TABP.API.Contracts.Reviews
{
    public record UpdateReviewRequest
    (
        long HotelId,
        int Rating,
        string Comment
    );
}