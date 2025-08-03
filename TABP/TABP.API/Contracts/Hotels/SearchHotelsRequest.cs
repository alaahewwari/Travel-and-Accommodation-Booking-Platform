namespace TABP.API.Contracts.Hotels
{
    public class SearchHotelsRequest 
    {
        public string? Filters { get; set; }
        public string? Sorts { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public DateTime CheckInDate { get; set; } = DateTime.Today;
        public DateTime CheckOutDate { get; set; } = DateTime.Today.AddDays(1);
        public int Adults { get; set; } = 2;
        public int Children { get; set; } = 0;
        public int Rooms { get; set; } = 1;
    }
}