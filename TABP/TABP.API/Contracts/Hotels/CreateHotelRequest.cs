namespace TABP.API.Contracts.Hotels
{
    public record CreateHotelRequest(
    string Name,
    string? Description,
    string BriefDescription,
    string Address,
    byte StarRating,
    double LocationLatitude,
    double LocationLongitude,
    int CityId,
    long OwnerId
    );
}