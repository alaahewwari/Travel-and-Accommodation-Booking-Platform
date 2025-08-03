using Sieve.Models;
using TABP.Domain.Entities;
using TABP.Domain.Models;
namespace TABP.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for booking data access operations including CRUD operations, filtering, and availability checking.
    /// Provides methods for booking management, reservation queries, overlap detection, and user-specific booking operations.
    /// </summary>
    public interface IBookingRepository
    {
        /// <summary>
        /// Creates a new booking in the repository with the provided booking information.
        /// Adds the booking to the database and persists the changes immediately for reservation management.
        /// </summary>
        /// <param name="booking">The booking entity to create with all required properties including rooms, dates, and user information.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The created booking entity with generated ID and database-assigned values.</returns>
        Task<Booking> CreateAsync(Booking booking, CancellationToken cancellationToken);
        /// <summary>
        /// Cancels an existing booking by updating its status to cancelled.
        /// Performs a soft cancellation that maintains the booking record for historical purposes and refund processing.
        /// </summary>
        /// <param name="booking">The booking entity to cancel with its current information.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>A task representing the asynchronous cancellation operation.</returns>
        Task CancelAsync(Booking booking, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves bookings for a specific user with advanced filtering, sorting, and pagination capabilities.
        /// Returns user-specific bookings with applied filters using Sieve processing for dynamic query operations.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to retrieve bookings for.</param>
        /// <param name="sieveModel">Sieve model containing filters, sorts, and pagination parameters for dynamic query processing.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// A paged result containing bookings that belong to the specified user with pagination metadata including total count and page information.
        /// </returns>
        Task<PagedResult<Booking>> GetBookingsAsync(long userId, SieveModel sieveModel, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves a booking by its unique identifier including all related entities for detailed operations.
        /// Performs a read-only query to find the booking with complete information including rooms, hotel, user, and invoice data.
        /// </summary>
        /// <param name="id">The unique identifier of the booking to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The booking with the specified ID and all related entities if found; otherwise, null if no booking exists with that ID.
        /// </returns>
        Task<Booking?> GetByIdAsync(long id, CancellationToken cancellationToken);
        /// <summary>
        /// Checks if any of the specified rooms have booking conflicts for the given date range.
        /// Validates room availability by detecting overlapping bookings that are not cancelled or completed.
        /// </summary>
        /// <param name="roomIds">Collection of room identifiers to check for availability conflicts.</param>
        /// <param name="checkIn">The check-in date to validate availability from.</param>
        /// <param name="checkOut">The check-out date to validate availability until.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// True if there are overlapping bookings for any of the specified rooms in the given date range; false if all rooms are available.
        /// </returns>
        Task<bool> CheckBookingOverlapAsync(IEnumerable<long> roomIds, DateTime checkIn, DateTime checkOut, CancellationToken cancellationToken);
    }
}