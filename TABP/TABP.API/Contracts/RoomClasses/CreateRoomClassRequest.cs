namespace TABP.API.Contracts.RoomClasses
{
    /// <summary>
    /// Request contract for creating a new room class within a hotel.
    /// Contains all required information to define a new room category with its specifications and pricing.
    /// </summary>
    /// <param name="Type">Numeric identifier for the room class type (e.g., 1=Standard, 2=Deluxe, 3=Suite).</param>
    /// <param name="Description">Optional detailed description of the room class features and amenities.</param>
    /// <param name="BriefDescription">Short summary description of the room class for quick reference.</param>
    /// <param name="PricePerNight">Base price per night for this room class in the local currency.</param>
    /// <param name="AdultsCapacity">Maximum number of adults that can stay in this room class.</param>
    /// <param name="ChildrenCapacity">Maximum number of children that can stay in this room class.</param>
    /// <param name="HotelId">Identifier of the hotel this room class belongs to.</param>
    public record CreateRoomClassRequest(
        byte Type,
        string? Description,
        string BriefDescription,
        decimal PricePerNight,
        int AdultsCapacity,
        int ChildrenCapacity,
        long HotelId
        );
}