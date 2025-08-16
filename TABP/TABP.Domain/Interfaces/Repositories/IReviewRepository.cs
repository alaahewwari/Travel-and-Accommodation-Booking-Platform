using TABP.Domain.Entities;
namespace TABP.Domain.Interfaces.Repositories
{

    /// <summary>
    /// Repository interface for review data access operations including CRUD operations 
    /// and hotel‐scoped queries. Provides methods for creating, retrieving, updating, 
    /// and deleting reviews, as well as listing reviews by hotel.
    /// </summary>
    public interface IReviewRepository
    {
        /// <summary>
        /// Creates a new review in the repository with the provided review information.
        /// Persists the review to the database immediately.
        /// </summary>
        /// <param name="review">The review entity to create with all required properties populated.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The created review entity with generated ID and database‐assigned values.</returns>
        Task<Review> CreateReviewAsync(Review review, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a review by its unique identifier.
        /// Performs a read‐only query to find the review with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the review to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The review with the specified ID if found; otherwise, null.
        /// </returns>
        Task<Review?> GetReviewByIdAsync(long id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves all reviews for a given hotel.
        /// Returns a collection of reviews associated with the specified hotel ID.
        /// </summary>
        /// <param name="hotelId">The unique identifier of the hotel whose reviews to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// A collection of reviews for the specified hotel.
        /// </returns>
        Task<IEnumerable<Review>> GetReviewsByHotelIdAsync(long hotelId, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing review’s information in the repository.
        /// Persists changes to the database.
        /// </summary>
        /// <param name="review">The review entity with updated information, including its ID.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// True if the review was successfully updated, otherwise false if no review affected.
        /// </returns>
        Task<bool> UpdateReviewAsync(Review review, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a review from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the review to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// True if the review was successfully deleted; false if no review was found.
        /// </returns>
        Task DeleteReviewAsync(Review review, CancellationToken cancellationToken);
        /// <summary>
        /// Calculates the average rating for all reviews of a specific hotel.
        /// Returns 0 if the hotel has no reviews.
        /// </summary>
        /// <param name="hotelId">The unique identifier of the hotel.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The average rating as a double, or 0 if no reviews are found.</returns>
        Task<double> GetAverageRatingAsync(long hotelId, CancellationToken cancellationToken);
    }
}