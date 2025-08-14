namespace TABP.Application.Cities.Common
{
    public record TrendingCityResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Country { get; init; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public int VisitCount { get; set; }
    }
}