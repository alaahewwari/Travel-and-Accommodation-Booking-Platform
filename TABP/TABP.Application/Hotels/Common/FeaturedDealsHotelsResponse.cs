namespace TABP.Application.Hotels.Common
{
    public record FeaturedDealsHotelsResponse
    (
        long HotelId,
        string HotelName,
        string HotelAddress,
        double LocationLatitude,
        double LocationLongitude,
        string ThumbnailImageUrl,
        decimal OriginalPrice,
        decimal DiscountedPrice
    );
}