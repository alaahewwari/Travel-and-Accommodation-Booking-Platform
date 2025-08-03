namespace TABP.Domain.Models
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