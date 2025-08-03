namespace TABP.Domain.Enums
{
    /// <summary>
    /// Supported payment providers for processing transactions.
    /// </summary>
    public enum PaymentProvider: byte
    {
        /// <summary>
        /// Stripe payment processing platform.
        /// </summary>
        Stripe = 1,
        /// <summary>
        /// PayPal payment processing platform.
        /// </summary>
        PayPal = 2,
        /// <summary>
        /// Square payment processing platform.
        /// </summary>
        Square = 3
    }
}