namespace TABP.API.Contracts.Reviews
{
    /// <summary>
    /// Request contract for updating an existing review for a hotel.
    /// Contains the target hotel identifier, updated rating, and revised comment text.
    /// </summary>
    /// <param name="HotelId">The unique identifier of the hotel being reviewed.</param>
    /// <param name="Rating">The updated rating for the hotel, typically between 1 (worst) and 5 (best).</param>
    /// <param name="Comment">The updated free-text feedback about the hotel.</param>
    public record UpdateReviewRequest
    (
        long HotelId,
        int Rating,
        string Comment
    );
}