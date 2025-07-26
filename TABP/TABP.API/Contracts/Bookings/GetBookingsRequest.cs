namespace TABP.API.Contracts.Bookings
{
    public record GetBookingsRequest(
        string? Filters,
        string? Sorts,
        int? Page,
        int? PageSize
    );
}
