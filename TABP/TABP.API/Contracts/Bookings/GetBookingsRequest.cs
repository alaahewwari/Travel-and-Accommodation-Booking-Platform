namespace TABP.API.Contracts.Bookings
{
    /// <summary>
    /// Request contract for retrieving bookings with filtering and pagination.
    /// Contains parameters for searching, sorting, and paginating booking records.
    /// </summary>
    /// <param name="Filters">Optional filter criteria for narrowing booking results (e.g., status, date range, hotel).</param>
    /// <param name="Sorts">Optional sorting parameters for ordering booking results (e.g., date, status, amount).</param>
    /// <param name="Page">Optional page number for pagination of booking results.</param>
    /// <param name="PageSize">Optional number of bookings per page for pagination control.</param>
    public record GetBookingsRequest(
        string? Filters,
        string? Sorts,
        int? Page,
        int? PageSize
    );
}