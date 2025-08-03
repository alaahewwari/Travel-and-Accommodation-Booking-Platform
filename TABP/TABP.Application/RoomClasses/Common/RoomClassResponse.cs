namespace TABP.Application.RoomClasses.Common
{
    public record RoomClassResponse(
        long Id,
        string Description,
        string BriefDescription,
        double PricePerNight,
        int AdultsCapacity,
        int ChildrenCapacity,
        DateTime CreatedAt,
        DateTime? UpdatedAt
    );
}