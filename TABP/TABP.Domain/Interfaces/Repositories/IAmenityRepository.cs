using TABP.Domain.Entities;
namespace TABP.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for amenity data access operations including CRUD operations and name-based lookups.
    /// Provides methods for amenity management, hotel feature assignment, and room class amenity operations.
    /// </summary>
    public interface IAmenityRepository
    {
        /// <summary>
        /// Creates a new amenity in the repository with the provided amenity information.
        /// Adds the amenity to the database and persists the changes immediately for hotel feature management.
        /// </summary>
        /// <param name="amenity">The amenity entity to create with all required properties including name and description.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The created amenity entity with generated ID and database-assigned values.</returns>
        Task<Amenity> CreateAmenityAsync(Amenity amenity, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves all active amenities in the system excluding soft-deleted entries.
        /// Returns amenities available for assignment to hotels and room classes for feature management.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>A collection of active amenities available for hotel and room class assignment.</returns>
        Task<IEnumerable<Amenity>> GetAllAmenitiesAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves an amenity by its unique identifier for detailed operations and management.
        /// Performs a read-only query to find the amenity with complete information.
        /// </summary>
        /// <param name="id">The unique identifier of the amenity to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The amenity with the specified ID if found; otherwise, null if no amenity exists with that ID.
        /// </returns>
        Task<Amenity?> GetAmenityByIdAsync(long id, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves an amenity by its name for validation and duplicate checking operations.
        /// Performs a case-sensitive name lookup to find existing amenities and prevent duplicates.
        /// </summary>
        /// <param name="name">The name of the amenity to search for.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The amenity with the specified name if found; otherwise, null if no amenity exists with that name.
        /// </returns>
        Task<Amenity?> GetAmenityByNameAsync(string name, CancellationToken cancellationToken);
        /// <summary>
        /// Updates an existing amenity's information in the repository.
        /// Performs an efficient bulk update operation for amenity details and returns the updated amenity with current data.
        /// </summary>
        /// <param name="amenity">The amenity entity with updated information including the ID of the amenity to update.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The updated amenity entity with current data if the update was successful; otherwise, null if no amenity was found to update.
        /// </returns>
        Task<Amenity?> UpdateAmenityAsync(Amenity amenity, CancellationToken cancellationToken);
        /// <summary>
        /// Performs a soft delete on an amenity by marking it as deleted without removing it from the database.
        /// Maintains amenity references for historical bookings while preventing future assignments.
        /// </summary>
        /// <param name="id">The unique identifier of the amenity to soft delete.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// True if the amenity was successfully marked as deleted; false if no amenity was found with the specified ID.
        /// </returns>
        Task<bool> SoftDeleteAmenityAsync(long id, CancellationToken cancellationToken);
    }
}