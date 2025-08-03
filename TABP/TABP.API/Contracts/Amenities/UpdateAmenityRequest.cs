namespace TABP.API.Contracts.Amenities
{
    /// <summary>
    /// Request contract for updating hotel or room amenity information.
    /// Contains modifiable properties of an amenity including name and description.
    /// </summary>
    /// <param name="Name">The updated name of the amenity (e.g., WiFi, Pool, Spa).</param>
    /// <param name="Description">The updated detailed description of the amenity and its features.</param>
    public record UpdateAmenityRequest(string Name, string Description);
}