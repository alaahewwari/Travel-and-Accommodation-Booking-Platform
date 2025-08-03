namespace TABP.Application.Discounts.Common
{
    public record DiscountResponse(
        int Id,
        int Percentage,
        DateTimeOffset StartDate,
        DateTimeOffset EndDate
    );
}