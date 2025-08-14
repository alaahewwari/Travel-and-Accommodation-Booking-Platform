namespace TABP.API.Contracts.Cities
{
    /// <summary>
    /// Request contract for updating city information.
    /// Contains modifiable properties of a city including location and postal details.
    /// </summary>
    /// <param name="Name">The updated name of the city.</param>
    /// <param name="Country">The updated country where the city is located.</param>
    /// <param name="PostOffice">The updated postal or zip code for the city.</param>
    public record UpdateCityRequest(string? Name, string? Country, string? PostOffice);
}