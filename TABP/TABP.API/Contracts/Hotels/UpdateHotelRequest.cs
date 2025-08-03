namespace TABP.API.Contracts.Hotels
{
    public record UpdateHotelRequest(
    string Name,
    string Address,
    double LocationLatitude,
    double LocationLongitude,
    int CityId,
    long OwnerId
    );
}