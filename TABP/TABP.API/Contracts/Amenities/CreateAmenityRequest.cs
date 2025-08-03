namespace TABP.API.Contracts.Amenities
{
    /// <summary>
    /// Request contract for creating a new amenity in the system.
    /// Contains essential information required for amenity registration.
    /// </summary>
    /// <param name="Name">The name of the amenity (e.g., WiFi, Pool, Spa).</param>
    /// <param name="Description">Detailed description of the amenity and its features.</param>
    public record CreateAmenityRequest(string Name, string Description);
}