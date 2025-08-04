namespace TABP.API.Contracts.Reviews
{
    public record CreateReviewRequest(
        long HotelId,
        int Rating,
        string Comment
    );
}