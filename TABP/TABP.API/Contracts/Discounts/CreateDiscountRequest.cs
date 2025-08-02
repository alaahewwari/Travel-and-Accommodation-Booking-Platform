namespace TABP.API.Contracts.Discounts
{
    /// <summary>
    /// Request contract for creating a new discount offer.
    /// Contains discount parameters including percentage and validity period.
    /// </summary>
    /// <param name="Percentage">The discount percentage to be applied (e.g., 20 for 20% off).</param>
    /// <param name="StartDate">The date when the discount becomes active and applicable.</param>
    /// <param name="EndDate">The date when the discount expires and is no longer valid.</param>
    public record CreateDiscountRequest(
        int Percentage,
        DateTime StartDate,
        DateTime EndDate
        );
}