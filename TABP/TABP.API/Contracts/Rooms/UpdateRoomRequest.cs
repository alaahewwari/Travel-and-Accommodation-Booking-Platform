namespace TABP.API.Contracts.Rooms
{
    /// <summary>
    /// Request contract for updating an existing room's information.
    /// Contains modifiable properties of a hotel room.
    /// </summary>
    /// <param name="Number">The room number or identifier within the hotel.</param>
    public record UpdateRoomRequest(string Number);
}