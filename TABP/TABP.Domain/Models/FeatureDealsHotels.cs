namespace TABP.Domain.Models
{
    public record FeaturedealsHotels(
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