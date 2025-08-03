namespace TABP.API.Contracts.RoomClasses
{
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