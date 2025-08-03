namespace TABP.Application.Hotels.Common
{
    public record HotelFeaturedDealsResponse
    (
        long Id,
        string Name,
        string Location,
        string ThumbnailUrl,
        double OriginalPrice,
        double DiscountedPrice,
        byte StarRating
    );
}