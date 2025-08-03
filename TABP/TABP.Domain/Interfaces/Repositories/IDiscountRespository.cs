using TABP.Domain.Entities;
namespace TABP.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for discount data access operations including CRUD operations and room class assignment management.
    /// Provides methods for discount creation, retrieval, deletion, and assignment to room classes for pricing management.
    /// </summary>
    public interface IDiscountRepository
    {
        /// <summary>
        /// Creates a new discount and assigns it to a specific room class in a single transaction.
        /// Establishes the relationship between the discount and room class while persisting the discount to the database.
        /// </summary>
        /// <param name="discount">The discount entity to create with all required properties including percentage and validity dates.</param>
        /// <param name="roomClass">The room class entity to assign the discount to for pricing calculations.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The created discount entity with generated ID and established room class relationship.</returns>
        Task<Discount> CreateAndAssignDiscountAsync(Discount discount, RoomClass roomClass, CancellationToken cancellationToken);
        /// <summary>
        /// Deletes a discount from the repository by its unique identifier.
        /// Removes the discount and all associated room class relationships from the database permanently.
        /// </summary>
        /// <param name="id">The unique identifier of the discount to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// True if the discount was successfully deleted; false if no discount was found with the specified ID.
        /// </returns>
        Task<bool> DeleteDiscountAsync(int id, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves a discount by its unique identifier for viewing and management operations.
        /// Performs a read-only query to find the discount with its basic information.
        /// </summary>
        /// <param name="id">The unique identifier of the discount to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// The discount with the specified ID if found; otherwise, null if no discount exists with that ID.
        /// </returns>
        Task<Discount?> GetDiscountByIdAsync(int id, CancellationToken cancellationToken);
    }
}