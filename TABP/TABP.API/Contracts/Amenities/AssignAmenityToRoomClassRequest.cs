namespace TABP.API.Contracts.Amenities
{
    /// <summary>
    /// Request contract for assigning an amenity to a specific room class.
    /// Contains the amenity identifier to be associated with a room class.
    /// </summary>
    /// <param name="AmenityId">The identifier of the amenity to be assigned to the room class.</param>
    public record AssignAmenityToRoomClassRequest(long AmenityId);
}