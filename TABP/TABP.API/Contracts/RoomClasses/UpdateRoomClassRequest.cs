namespace TABP.API.Contracts.RoomClasses
{
    public record UpdateRoomClassRequest(
        string Description,
        int AdultsCapacity,
        int ChildrenCapacity,
        decimal PricePerNight
        );
}