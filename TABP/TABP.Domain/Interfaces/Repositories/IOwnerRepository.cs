using TABP.Domain.Entities;
namespace TABP.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for hotel owner data access operations including CRUD operations and owner management.
    /// Provides methods for owner creation, retrieval, updates, deletion, and owner-specific queries for hotel management.
    /// </summary>
    public interface IOwnerRepository
    {
        /// <summary>
        /// Creates a new hotel owner in the repository with the provided owner information.
        /// Adds the owner to the database and persists the changes immediately.
        /// </summary>
        /// <param name="owner">The owner entity to create with all required properties populated including personal and contact information.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The created owner entity with generated ID and database-assigned values.</returns>
        Task<Owner> CreateOwnerAsync(Owner owner, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves a hotel owner by their unique identifier for profile and management operations.
        /// Performs a read-only query to find the owner with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the owner to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The owner with the specified ID if found; otherwise, null if no owner exists with that ID.
        /// </returns>
        Task<Owner?> GetOwnerByIdAsync(long id, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves all hotel owners in the system for administrative and management purposes.
        /// Returns complete list of owners with their personal and contact information.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>A collection of all hotel owners in the system with their complete information.</returns>
        Task<IEnumerable<Owner>> GetAllOwnersAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Updates an existing hotel owner's information in the repository.
        /// Modifies the owner data including personal and contact details and persists the changes to the database.
        /// </summary>
        /// <param name="owner">The owner entity with updated information including the ID of the owner to update.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The updated owner entity with current data if the update was successful; otherwise, null if no owner was found to update.
        /// </returns>
        Task<Owner?> UpdateOwnerAsync(Owner owner, CancellationToken cancellationToken);
        /// <summary>
        /// Deletes a hotel owner from the repository by their unique identifier.
        /// Removes the owner from the database permanently if they exist and have no associated hotels.
        /// </summary>
        /// <param name="id">The unique identifier of the owner to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// True if the owner was successfully deleted; false if no owner was found with the specified ID or if deletion constraints prevent removal.
        /// </returns>
        Task<bool> DeleteOwnerAsync(long id, CancellationToken cancellationToken);
    }
}