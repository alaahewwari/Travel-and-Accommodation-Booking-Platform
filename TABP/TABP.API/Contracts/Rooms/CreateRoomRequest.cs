namespace TABP.API.Contracts.Rooms
{
    /// <summary>
    /// Request contract for creating a new room within a hotel.
    /// Contains essential information required for room creation.
    /// </summary>
    /// <param name="Number">The room number or identifier within the hotel.</param>
    public record CreateRoomRequest(string Number);
}