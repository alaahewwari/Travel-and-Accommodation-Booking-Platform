namespace TABP.API.Contracts.Hotels
{
    /// <summary>
    /// Request contract for creating a new hotel in the system.
    /// Contains all required information including location, ownership, and property details.
    /// </summary>
    /// <param name="Name">The name of the hotel.</param>
    /// <param name="Description">Optional detailed description of the hotel facilities and services.</param>
    /// <param name="BriefDescription">Short summary description of the hotel for quick reference.</param>
    /// <param name="Address">The physical address of the hotel.</param>
    /// <param name="StarRating">The star rating of the hotel (1-5 stars).</param>
    /// <param name="LocationLatitude">The latitude coordinate for hotel location mapping.</param>
    /// <param name="LocationLongitude">The longitude coordinate for hotel location mapping.</param>
    /// <param name="CityId">The identifier of the city where the hotel is located.</param>
    /// <param name="OwnerId">The identifier of the hotel owner.</param>
    public record CreateHotelRequest(
    string Name,
    string? Description,
    string BriefDescription,
    string Address,
    byte StarRating,
    double LocationLatitude,
    double LocationLongitude,
    int CityId,
    long OwnerId
    );
}