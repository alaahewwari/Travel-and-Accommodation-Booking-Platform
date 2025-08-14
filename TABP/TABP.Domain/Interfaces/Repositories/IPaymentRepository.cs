using TABP.Domain.Entities;
namespace TABP.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for payment data access operations including transaction records and payment history.
    /// Provides methods for payment tracking, transaction management, and payment-related queries.
    /// </summary>
    public interface IPaymentRepository
    {
        /// <summary>
        /// Creates a new payment record in the repository with transaction details.
        /// Stores payment information for tracking, reconciliation, and audit purposes.
        /// </summary>
        /// <param name="payment">The payment entity to create with all transaction details including provider information.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The created payment entity with generated ID and database-assigned values.</returns>
        Task<Payment> CreateAsync(Payment payment, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves a payment by its unique identifier for transaction details and status checking.
        /// Performs a read-only query to find the payment with complete information.
        /// </summary>
        /// <param name="id">The unique identifier of the payment to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The payment with the specified ID if found; otherwise, null if no payment exists with that ID.</returns>
        Task<Payment?> GetByIdAsync(long id, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves a payment by the payment provider's intent identifier for status updates and confirmations.
        /// Looks up payments using external payment provider identifiers for webhook processing.
        /// </summary>
        /// <param name="paymentIntentId">The payment intent identifier from the payment provider.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The payment with the specified intent ID if found; otherwise, null if no payment exists with that intent ID.</returns>
        Task<Payment?> GetByPaymentIntentIdAsync(string paymentIntentId, CancellationToken cancellationToken);
        /// <summary>
        /// Updates an existing payment's status and details after processing or status changes.
        /// Modifies payment information for status updates, confirmations, and failure handling.
        /// </summary>
        /// <param name="payment">The payment entity with updated information including the ID of the payment to update.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>The updated payment entity with current data if successful; otherwise, null if no payment was found.</returns>
        Task<Payment?> UpdateAsync(Payment payment, CancellationToken cancellationToken);
        /// <summary>
        /// Retrieves all payments associated with a specific booking for transaction history and reconciliation.
        /// Returns payment records for a booking including successful payments, failed attempts, and refunds.
        /// </summary>
        /// <param name="bookingId">The unique identifier of the booking to get payments for.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>A collection of payments associated with the specified booking ordered by creation date.</returns>
        Task<IEnumerable<Payment>> GetByBookingIdAsync(long bookingId, CancellationToken cancellationToken);
    }
}