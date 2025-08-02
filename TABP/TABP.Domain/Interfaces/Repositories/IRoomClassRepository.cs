using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel;
namespace TABP.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for room class data access operations including CRUD operations, discount management, and amenity assignments.
    /// Provides methods for room class management, featured deals retrieval, and room class-amenity relationship handling.
    /// </summary>
    public interface IRoomClassRepository
    {
        /// <summary>
        /// Creates a new room class in the repository with the provided room class information.
        /// Adds the room class to the database and persists the changes immediately.
        /// </summary>
        /// <param name="roomClass">The room class entity to create with all required properties populated including hotel reference and pricing.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The created room class entity with generated ID and database-assigned values.</returns>
        Task<RoomClass> CreateRoomClassAsync(RoomClass roomClass, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves a room class by its unique identifier with all related data.
        /// Performs a query to find the room class with complete information for detailed operations.
        /// </summary>
        /// <param name="id">The unique identifier of the room class to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The room class with the specified ID if found; otherwise, null if no room class exists with that ID.
        /// </returns>
        Task<RoomClass?> GetRoomClassByIdAsync(long id, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves all room classes in the system for administrative and management purposes.
        /// Returns complete list of room classes with their associated information.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>A collection of all room classes in the system with their complete information.</returns>
        Task<IEnumerable<RoomClass>> GetAllRoomClassesAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Updates an existing room class's information in the repository.
        /// Modifies the room class data and persists the changes to the database.
        /// </summary>
        /// <param name="roomClass">The room class entity with updated information including the ID of the room class to update.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The updated room class entity with current data if the update was successful; otherwise, null if no room class was found to update.
        /// </returns>
        Task<RoomClass?> UpdateRoomClassAsync(RoomClass roomClass, CancellationToken cancellationToken);
        /// <summary>
        /// Deletes a room class from the repository.
        /// Removes the specified room class from the database permanently.
        /// </summary>
        /// <param name="roomClass">The room class entity to delete from the repository.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        Task DeleteRoomClassAsync(RoomClass roomClass, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves featured hotel deals based on room classes with active discounts and promotions.
        /// Returns a limited number of hotels with the best deals for promotional and marketing purposes.
        /// </summary>
        /// <param name="count">The maximum number of featured hotel deals to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// A collection of featured hotel deals with room class information, limited to the specified count, ordered by deal attractiveness.
        /// </returns>
        Task<IEnumerable<FeaturedealsHotels>> GetFeaturedDealsInHotelsAsync(int count, CancellationToken cancellationToken);
        /// <summary>
        /// Creates an association between an amenity and a room class.
        /// Establishes a many-to-many relationship linking the specified amenity to the room class.
        /// </summary>
        /// <param name="amenity">The amenity entity to assign to the room class.</param>
        /// <param name="roomClass">The room class entity that will receive the amenity assignment.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>A task representing the asynchronous assignment operation.</returns>
        Task AssignAmenityToRoomClassAsync(Amenity amenity, RoomClass roomClass, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves the active discount associated with a specific room class.
        /// Searches for current and valid discounts that apply to the specified room class.
        /// </summary>
        /// <param name="roomClassId">The unique identifier of the room class to get the discount for.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The active discount for the specified room class if one exists and is currently valid; otherwise, null if no active discount is found.
        /// </returns>
        Task<Discount?> GetDiscountByRoomClassAsync(long roomClassId, CancellationToken cancellationToken);
    }
}