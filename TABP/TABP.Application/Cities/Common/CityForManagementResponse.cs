namespace TABP.Application.Cities.Common
{
    public record CityForManagementResponse(
         int Id,
         string Name,
         string Country,
         string PostOffice,
         DateTime CreatedAt,
         DateTime UpdatedAt,
         int NumberOfHotels
     );
}