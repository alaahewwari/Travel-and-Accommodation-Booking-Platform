namespace TABP.API.Contracts.Hotels
{
    /// <summary>
    /// Request contract for retrieving hotels with filtering and pagination.
    /// Contains parameters for searching, sorting, and paginating hotel records.
    /// </summary>
    /// <param name="Filters">Optional filter criteria for narrowing hotel results (e.g., location, rating, availability).</param>
    /// <param name="Sorts">Optional sorting parameters for ordering hotel results (e.g., name, rating, price).</param>
    /// <param name="Page">Optional page number for pagination of hotel results.</param>
    /// <param name="PageSize">Optional number of hotels per page for pagination control.</param>
    public record GetHotelsRequest(
        string? Filters,
        string? Sorts,
        int? Page,
        int? PageSize
    );
}