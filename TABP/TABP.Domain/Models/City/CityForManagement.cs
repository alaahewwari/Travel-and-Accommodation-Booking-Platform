namespace TABP.Domain.Models.City
{
    public record CityForManagement(
        int Id,
        string Name,
        string Country,
        string PostOffice,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        int NumberOfHotels
        );
}