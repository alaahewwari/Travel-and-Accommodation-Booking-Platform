namespace TABP.API.Contracts.Discounts
{
    public record CreateDiscountRequest(
        int Percentage,
        DateTime StartDate,
        DateTime EndDate
        );
}