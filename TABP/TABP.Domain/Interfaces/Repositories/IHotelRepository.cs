using Sieve.Models;
using TABP.Domain.Entities;
using TABP.Domain.Models.Common;
using TABP.Domain.Models.Hotel;
namespace TABP.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for hotel data access operations including CRUD operations, search functionality, and location-based queries.
    /// Provides methods for hotel management, advanced search with filtering, pagination, and user history tracking.
    /// </summary>
    public interface IHotelRepository
    {
        /// <summary>
        /// Creates a new hotel in the repository with the provided hotel information.
        /// Adds the hotel to the database and persists the changes immediately.
        /// </summary>
        /// <param name="hotel">The hotel entity to create with all required properties populated including location, owner, and amenities.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The created hotel entity with generated ID and database-assigned values.</returns>
        Task<Hotel> CreateHotelAsync(Hotel hotel, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves a hotel by its unique identifier for detailed operations and management.
        /// Performs a read-only query to find the hotel with complete information.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The hotel with the specified ID if found; otherwise, null if no hotel exists with that ID.
        /// </returns>
        Task<Hotel?> GetHotelByIdAsync(long id, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves all hotels in the system with advanced filtering, sorting, and pagination capabilities using Sieve processing.
        /// Returns a list of hotels formatted for management and administrative purposes, including room statistics, ratings, and ownership information.
        /// </summary>
        /// <param name="sieveModel">Sieve model containing filters, sorts, and pagination parameters for dynamic query processing.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// A collection of hotels formatted for management operations, enriched with relevant metadata such as owner details, room counts, and performance metrics.
        /// </returns>
        Task<PagedResult<HotelForManagement>> GetAllHotelsAsync(SieveModel sieveModel, CancellationToken cancellationToken);
        /// <summary>
        /// Updates an existing hotel's information in the repository.
        /// Performs an efficient bulk update operation and returns the updated hotel with current data.
        /// </summary>
        /// <param name="hotel">The hotel entity with updated information including the ID of the hotel to update.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The updated hotel entity with current data if the update was successful; otherwise, null if no hotel was found to update.
        /// </returns>
        Task<Hotel?> UpdateHotelAsync(Hotel hotel, CancellationToken cancellationToken);
        /// <summary>
        /// Deletes a hotel from the repository by its unique identifier.
        /// Removes the hotel from the database permanently if it exists and meets deletion criteria.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// True if the hotel was successfully deleted; false if no hotel was found with the specified ID.
        /// </returns>
        Task<bool> DeleteHotelAsync(long id, CancellationToken cancellationToken);
        /// <summary>
        /// Checks if a hotel exists at the specified geographic coordinates.
        /// Performs location-based validation to prevent duplicate hotels at the same location.
        /// </summary>
        /// <param name="latitude">The latitude coordinate to check for existing hotels.</param>
        /// <param name="longitude">The longitude coordinate to check for existing hotels.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// True if a hotel exists at the specified coordinates; false if no hotel is found at that location.
        /// </returns>
        Task<bool> GetHotelByLocationAsync(double latitude, double longitude, CancellationToken cancellationToken);
        /// <summary>
        /// Performs advanced hotel search with filtering, sorting, and pagination capabilities.
        /// Searches hotels based on guest capacity requirements and applies dynamic filters using Sieve processing.
        /// </summary>
        /// <param name="parameters">Search parameters including guest counts, dates, and capacity requirements.</param>
        /// <param name="sieveModel">Sieve model containing filters, sorts, and pagination parameters for dynamic query processing.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// A paged result containing hotels that match the search criteria with pagination metadata including total count and page information.
        /// </returns>
        Task<PagedResult<HotelSearchResultResponse>> SearchAsync(HotelSearchParameters parameters, SieveModel sieveModel, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves hotels that a specific user has previously visited based on completed bookings.
        /// Returns hotels ordered by most recent visit date for personalized recommendations and user history.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to get visited hotels for.</param>
        /// <param name="count">The maximum number of recently visited hotels to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// A collection of hotels that the user has previously booked, ordered by most recent visit date and limited to the specified count.
        /// </returns>
        Task<IEnumerable<Hotel>> GetRecentlyVisitedHotelsAsync(long userId, int count, CancellationToken cancellationToken);
    }
}