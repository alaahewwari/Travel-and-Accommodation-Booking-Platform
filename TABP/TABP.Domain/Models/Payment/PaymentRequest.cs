using TABP.Domain.Enums;
namespace TABP.Domain.Models.Payment
{
    /// <summary>
    /// Request model for processing a payment transaction containing all necessary payment information.
    /// </summary>
    public record PaymentRequest(
        /// <summary>
        /// Unique identifier of the booking being paid for.
        /// </summary>
        long BookingId,
        /// <summary>
        /// Payment amount in decimal format (e.g., 150.50 for $150.50).
        /// </summary>
        decimal Amount,
        /// <summary>
        /// Currency for the payment transaction.
        /// </summary>
        PriceCurrency Currency,
        /// <summary>
        /// Payment method ID obtained from frontend (e.g., pm_1234567890).
        /// </summary>
        string PaymentMethodId,
        /// <summary>
        /// Customer email address for receipt and communication.
        /// </summary>
        string CustomerEmail,
        /// <summary>
        /// Customer full name for payment processing.
        /// </summary>
        string CustomerName
    );
}