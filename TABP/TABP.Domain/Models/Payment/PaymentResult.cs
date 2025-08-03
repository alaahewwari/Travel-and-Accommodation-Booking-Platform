using TABP.Domain.Enums;
namespace TABP.Domain.Models.Payment
{
    /// <summary>
    /// Result model containing payment processing outcome and required information for frontend handling.
    /// </summary>
    public class PaymentResult
    {
        /// <summary>
        /// Indicates if the payment was successfully processed.
        /// </summary>
        public bool IsSuccess { get; private set; }
        /// <summary>
        /// Payment intent identifier from the payment provider.
        /// </summary>
        public string? PaymentIntentId { get; private set; }
        /// <summary>
        /// Client secret needed for frontend payment confirmation (when requires action).
        /// </summary>
        public string? ClientSecret { get; private set; }
        /// <summary>
        /// Current status of the payment.
        /// </summary>
        public PaymentStatus Status { get; private set; }
        /// <summary>
        /// Error message if payment failed.
        /// </summary>
        public string? ErrorMessage { get; private set; }
        /// <summary>
        /// Indicates if payment requires additional customer action (3D Secure, etc.).
        /// </summary>
        public bool RequiresAction { get; private set; }
        // Factory methods for creating results
        /// <summary>
        /// Creates a successful payment result.
        /// </summary>
        public static PaymentResult Success(string paymentIntentId)
        {
            return new PaymentResult
            {
                IsSuccess = true,
                PaymentIntentId = paymentIntentId,
                Status = PaymentStatus.Succeeded
            };
        }
        /// <summary>
        /// Creates a result indicating payment requires customer action.
        /// </summary>
        public static PaymentResult RequiresConfirmation(string paymentIntentId, string clientSecret)
        {
            return new PaymentResult
            {
                IsSuccess = false,
                RequiresAction = true,
                PaymentIntentId = paymentIntentId,
                ClientSecret = clientSecret,
                Status = PaymentStatus.RequiresAction
            };
        }
        /// <summary>
        /// Creates a failed payment result.
        /// </summary>
        public static PaymentResult Failed(string errorMessage, PaymentStatus status = PaymentStatus.Failed)
        {
            return new PaymentResult
            {
                IsSuccess = false,
                ErrorMessage = errorMessage,
                Status = status
            };
        }
        /// <summary>
        /// Creates a pending payment result.
        /// </summary>
        public static PaymentResult Pending(string paymentIntentId, string? clientSecret = null)
        {
            return new PaymentResult
            {
                IsSuccess = false,
                PaymentIntentId = paymentIntentId,
                ClientSecret = clientSecret,
                Status = PaymentStatus.Pending
            };
        }
    }
}