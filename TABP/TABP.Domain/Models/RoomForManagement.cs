namespace TABP.Domain.Models
{
    public record RoomForManagement(
        long Id,
        string Number,
        bool IsAvailable,
        int AdultsCapacity,
        int ChildrenCapacity,
        DateTime CreatedAtUtc,
        DateTime? ModifiedAtUtc
    );
}