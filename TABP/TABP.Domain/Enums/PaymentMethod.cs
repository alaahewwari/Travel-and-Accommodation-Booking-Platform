namespace TABP.Domain.Enums
{
    /// <summary>
    /// Payment method options.
    /// </summary>
    public enum PaymentMethod: byte
    {
        /// <summary>Credit card payment</summary>
        CreditCard = 1,
        /// <summary>Debit card payment</summary>
        DebitCard = 2,
        /// <summary>PayPal payment</summary>
        PayPal = 3,
        /// <summary>Cash payment</summary>
        Cash = 4,
    }
}