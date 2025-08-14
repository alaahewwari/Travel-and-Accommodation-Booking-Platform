using TABP.Domain.Entities;
using TABP.Domain.Models.City;
namespace TABP.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for city data access operations including CRUD operations, management queries, and analytics.
    /// Provides methods for city management, booking analytics, and administrative operations for travel destinations.
    /// </summary>
    public interface ICityRepository
    {
        /// <summary>
        /// Creates a new city in the repository with the provided city information.
        /// Adds the city to the database and persists the changes immediately for travel destination management.
        /// </summary>
        /// <param name="city">The city entity to create with all required properties including name, country, and postal information.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The created city entity with generated ID and database-assigned values.</returns>
        Task<City> CreateCityAsync(City city, CancellationToken cancellationToken);
        /// <summary>
        /// Deletes a city from the repository by its unique identifier.
        /// Removes the city from the database permanently if it exists and has no associated hotels or dependencies.
        /// </summary>
        /// <param name="cityId">The unique identifier of the city to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// True if the city was successfully deleted; false if no city was found with the specified ID.
        /// </returns>
        Task<bool> DeleteCityAsync(int cityId, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves all cities in the system formatted for management and administrative purposes.
        /// Returns cities with essential management information including hotel counts, creation dates, and location details.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>A collection of cities formatted for management operations with hotel statistics and administrative data.</returns>
        Task<IEnumerable<CityForManagement>> GetAllCitiesAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves a city by its unique identifier including associated images for detailed operations.
        /// Performs a read-only query to find the city with complete information including city images.
        /// </summary>
        /// <param name="cityId">The unique identifier of the city to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The city with the specified ID and its associated images if found; otherwise, null if no city exists with that ID.
        /// </returns>
        Task<City?> GetCityByIdAsync(int cityId, CancellationToken cancellationToken);
        /// <summary>
        /// Updates an existing city's information in the repository.
        /// Performs an efficient bulk update operation for city details and returns the updated city with current data.
        /// </summary>
        /// <param name="city">The city entity with updated information including the ID of the city to update.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The updated city entity with current data if the update was successful; otherwise, null if no city was found to update.
        /// </returns>
        Task<City?> UpdateCityAsync(City city, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves the most popular cities based on hotel booking activity for analytics and recommendations.
        /// Returns cities ordered by total booking count across all hotels within each city, useful for trending destinations.
        /// </summary>
        /// <param name="count">The maximum number of most booked cities to retrieve for analytics or display purposes.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// A collection of cities ordered by booking popularity, limited to the specified count, including hotel and booking information.
        /// </returns>
        Task<IEnumerable<City>> GetMostBookedCitiesAsync(int count, CancellationToken cancellationToken);
    }
}