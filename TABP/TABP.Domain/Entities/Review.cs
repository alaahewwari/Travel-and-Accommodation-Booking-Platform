namespace TABP.Domain.Entities
{
    public class Review
    {
        public long Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long UserId { get; set; }
        public long HotelId { get; set; }
        public User User { get; set; } = null!;
        public Hotel Hotel { get; set; } = null!;
    }
}