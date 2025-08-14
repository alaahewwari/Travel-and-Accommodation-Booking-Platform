namespace TABP.Domain.Enums
{
    /// <summary>
    /// Status values for payment transactions and processing states.
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// Payment is awaiting processing or confirmation.
        /// </summary>
        Pending = 1,
        /// <summary>
        /// Payment requires additional authentication (3D Secure, etc.).
        /// </summary>
        RequiresAction = 2,
        /// <summary>
        /// Payment has been successfully processed and completed.
        /// </summary>
        Succeeded = 3,
        /// <summary>
        /// Payment processing failed due to insufficient funds, card issues, etc.
        /// </summary>
        Failed = 4,
        /// <summary>
        /// Payment was cancelled before completion.
        /// </summary>
        Cancelled = 5,
        /// <summary>
        /// Payment has been refunded partially or fully.
        /// </summary>
        Refunded = 6
    }
}