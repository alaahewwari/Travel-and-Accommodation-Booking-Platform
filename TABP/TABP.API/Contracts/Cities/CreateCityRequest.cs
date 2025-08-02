namespace TABP.API.Contracts.Cities
{
    /// <summary>
    /// Request contract for creating a new city in the system.
    /// Contains essential location information required for city registration.
    /// </summary>
    /// <param name="Name">The name of the city.</param>
    /// <param name="Country">The country where the city is located.</param>
    /// <param name="PostOffice">The postal or zip code for the city.</param>
    public record CreateCityRequest(string Name, string Country, string PostOffice);
}