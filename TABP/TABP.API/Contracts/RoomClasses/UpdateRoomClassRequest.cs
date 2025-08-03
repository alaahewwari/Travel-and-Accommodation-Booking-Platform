namespace TABP.API.Contracts.RoomClasses
{
    /// <summary>
    /// Request contract for updating an existing room class definition.
    /// Contains modifiable properties that define room class characteristics and pricing.
    /// </summary>
    /// <param name="Description">Detailed description of the room class features and amenities.</param>
    /// <param name="AdultsCapacity">Maximum number of adults that can stay in this room class.</param>
    /// <param name="ChildrenCapacity">Maximum number of children that can stay in this room class.</param>
    /// <param name="PricePerNight">Base price per night for this room class in the local currency.</param>
    public record UpdateRoomClassRequest(
        string Description,
        int AdultsCapacity,
        int ChildrenCapacity,
        decimal PricePerNight
        );
}