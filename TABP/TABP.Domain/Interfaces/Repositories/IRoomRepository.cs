using TABP.Domain.Entities;
using TABP.Domain.Models.Room;
namespace TABP.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for room data access operations including CRUD operations and room management.
    /// Provides methods for room creation, retrieval, updates, deletion, and hotel-specific room queries.
    /// </summary>
    public interface IRoomRepository
    {
        /// <summary>
        /// Creates a new room in the repository with the provided room information.
        /// Adds the room to the database and persists the changes immediately.
        /// </summary>
        /// <param name="room">The room entity to create with all required properties populated.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The created room entity with generated ID and database-assigned values.</returns>
        Task<Room> CreateRoomAsync(Room room, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves a room by its unique identifier including its room class information.
        /// Performs a query with room class navigation property included for complete room details.
        /// </summary>
        /// <param name="id">The unique identifier of the room to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The room with the specified ID and its room class if found; otherwise, null if no room exists with that ID.
        /// </returns>
        Task<Room?> GetRoomByIdAsync(long id, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves all rooms in the system formatted for management and administrative purposes.
        /// Returns rooms with essential information including availability status and capacity details.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>A collection of rooms formatted for management operations with room class details.</returns>
        Task<IEnumerable<RoomForManagement>> GetAllRoomsAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Updates an existing room's information in the repository.
        /// Performs an efficient bulk update operation and returns the updated room with current data.
        /// </summary>
        /// <param name="room">The room entity with updated information including the ID of the room to update.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The updated room entity with current data if the update was successful; otherwise, null if no room was found to update.
        /// </returns>
        Task<Room?> UpdateRoomAsync(Room room, CancellationToken cancellationToken);
        /// <summary>
        /// Deletes a room from the repository by its unique identifier.
        /// Removes the room from the database permanently if it exists.
        /// </summary>
        /// <param name="id">The unique identifier of the room to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// True if the room was successfully deleted; false if no room was found with the specified ID.
        /// </returns>
        Task<bool> DeleteRoomAsync(long id, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves the first available room associated with a specific hotel.
        /// Searches for rooms that belong to room classes within the specified hotel.
        /// </summary>
        /// <param name="hotel">The hotel entity to search for associated rooms.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The first room found that belongs to the specified hotel; otherwise, null if no rooms are found for the hotel.
        /// </returns>
        Task<Room?> GetRoomByHotelAsync(Hotel hotel, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves a room by its unique identifier with room class information included.
        /// Alternative method for getting room with class details for specific use cases.
        /// </summary>
        /// <param name="id">The unique identifier of the room to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The room with the specified ID and its room class if found; otherwise, null if no room exists with that ID.
        /// </returns>
        Task<Room?> GetRoomWithClassByIdAsync(long id, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves multiple rooms by their unique identifiers within a specific hotel.
        /// Filters rooms to ensure they belong to the specified hotel and includes room class information.
        /// </summary>
        /// <param name="roomIds">Collection of room identifiers to retrieve.</param>
        /// <param name="HotelId">The hotel identifier to filter rooms by.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// A collection of rooms that match the provided IDs and belong to the specified hotel, including room class details.
        /// </returns>
        Task<IEnumerable<Room>> GetRoomsByIdsAsync(IEnumerable<long> roomIds, long HotelId, CancellationToken cancellationToken);
    }
}