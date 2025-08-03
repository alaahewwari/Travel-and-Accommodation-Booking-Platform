namespace TABP.Application.Rooms.Common
{
    public record RoomForManagementResponse(
        long Id,
        string Number,
        bool IsAvailable,
        int AdultsCapacity,
        int ChildrenCapacity,
        DateTime CreatedAtUtc,
        DateTime? ModifiedAtUtc
    );
}