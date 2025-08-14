namespace TABP.API.Contracts.Hotels
{
    /// <summary>
    /// Request contract for retrieving featured hotel deals for promotional display.
    /// Specifies the number of featured deals to return for homepage or marketing sections.
    /// </summary>
    /// <param name="Count">The number of featured hotel deals to retrieve.</param>
    public record HotelFeaturedDealsRequest(int Count);
}