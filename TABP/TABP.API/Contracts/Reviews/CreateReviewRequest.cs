namespace TABP.API.Contracts.Reviews
{
    /// <summary>
    /// Request contract for submitting a new review for a hotel.
    /// Contains the target hotel identifier, rating score, and an optional comment.
    /// </summary>
    /// <param name="HotelId">The unique identifier of the hotel being reviewed.</param>
    /// <param name="Rating">The rating given to the hotel, typically between 1 (worst) and 5 (best).</param>
    /// <param name="Comment">Optional free-text feedback about the hotel.</param>
    public record CreateReviewRequest(
        long HotelId,
        int Rating,
        string Comment
    );
}