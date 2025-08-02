namespace TABP.API.Contracts.Hotels
{
    /// <summary>
    /// Request contract for searching hotels with specific criteria and availability requirements.
    /// Contains filtering, sorting, pagination, and booking parameters for hotel search operations.
    /// </summary>
    public class SearchHotelsRequest
    {
        /// <summary>
        /// Optional filter criteria for narrowing search results (e.g., star rating, amenities, price range).
        /// </summary>
        public string? Filters { get; set; }
        /// <summary>
        /// Optional sorting parameters for ordering search results (e.g., price, rating, distance).
        /// </summary>
        public string? Sorts { get; set; }
        /// <summary>
        /// Optional page number for pagination of search results.
        /// </summary>
        public int? Page { get; set; }
        /// <summary>
        /// Optional number of results per page for pagination control.
        /// </summary>
        public int? PageSize { get; set; }
        /// <summary>
        /// Check-in date for availability search. Defaults to today's date.
        /// </summary>
        public DateTime CheckInDate { get; set; } = DateTime.Today;
        /// <summary>
        /// Check-out date for availability search. Defaults to tomorrow's date.
        /// </summary>
        public DateTime CheckOutDate { get; set; } = DateTime.Today.AddDays(1);
        /// <summary>
        /// Number of adult guests for room capacity requirements. Defaults to 2.
        /// </summary>
        public int Adults { get; set; } = 2;
        /// <summary>
        /// Number of children for room capacity requirements. Defaults to 0.
        /// </summary>
        public int Children { get; set; } = 0;
        /// <summary>
        /// Number of rooms required for the booking. Defaults to 1.
        /// </summary>
        public int Rooms { get; set; } = 1;
    }
}