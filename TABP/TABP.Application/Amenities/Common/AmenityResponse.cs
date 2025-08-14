namespace TABP.Application.Amenities.Common
{
    public record AmenityResponse(long Id, string Name, string Description, DateTime CreatedAt, DateTime UpdatedAt);
}