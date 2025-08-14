using TABP.Domain.Models.Payment;
namespace TABP.Domain.Interfaces.Services
{
    /// <summary>
    /// Service interface for payment processing operations including transactions, refunds, and confirmations.
    /// Provides methods for handling payment workflows across different payment providers.
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Processes a payment transaction for a booking reservation.
        /// Creates and confirms payment with the payment provider and handles authentication requirements.
        /// </summary>
        /// <param name="request">Payment request containing booking details, amount, and payment method information.</param>
        /// <returns>Payment result indicating success, failure, or additional action requirements.</returns>
        Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request);
        /// <summary>
        /// Confirms a payment that requires additional customer action such as 3D Secure authentication.
        /// Completes the payment process after customer authentication is successful.
        /// </summary>
        /// <param name="paymentIntentId">The payment intent identifier from the initial payment attempt.</param>
        /// <returns>Payment result indicating the final outcome of the confirmation process.</returns>
        Task<PaymentResult> ConfirmPaymentAsync(string paymentIntentId);
    }
}