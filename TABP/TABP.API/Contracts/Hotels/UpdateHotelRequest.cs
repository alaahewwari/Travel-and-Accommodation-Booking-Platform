namespace TABP.API.Contracts.Hotels
{
    /// <summary>
    /// Request contract for updating hotel information.
    /// Contains modifiable properties including location, ownership, and basic details.
    /// </summary>
    /// <param name="Name">The updated hotel name.</param>
    /// <param name="Address">The updated physical address of the hotel.</param>
    /// <param name="LocationLatitude">The updated latitude coordinate for hotel location.</param>
    /// <param name="LocationLongitude">The updated longitude coordinate for hotel location.</param>
    /// <param name="CityId">The identifier of the city where the hotel is located.</param>
    /// <param name="OwnerId">The identifier of the hotel owner.</param>
    public record UpdateHotelRequest(
    string Name,
    string Address,
    double LocationLatitude,
    double LocationLongitude,
    int CityId,
    long OwnerId
    );
}