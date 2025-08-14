namespace TABP.Domain.Enums
{
    /// <summary>
    /// Supported currencies for pricing and transactions.
    /// </summary>
    public enum PriceCurrency : int
    {
        /// <summary>
        /// US Dollar
        /// </summary>
        USD = 1,
        /// <summary>
        /// Euro
        /// </summary>
        EUR = 2,
        /// <summary>
        /// British Pound
        /// </summary>
        GBP = 3,
        /// <summary>
        /// Japanese Yen
        /// </summary>
        JPY = 4,
        /// <summary>
        /// Canadian Dollar
        /// </summary>
        CAD = 5,
    }
}