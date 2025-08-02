namespace TABP.API.Contracts.Cities
{
    /// <summary>
    /// Request contract for retrieving trending cities based on booking activity.
    /// Specifies the number of trending cities to return for promotional or recommendation purposes.
    /// </summary>
    /// <param name="Count">The number of trending cities to retrieve.</param>
    public record GetTrendingCitiesRequest(int Count);
}