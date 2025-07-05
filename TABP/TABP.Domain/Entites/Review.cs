namespace TABP.Domain.Entites
{
    public class Review
    {
        public long Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public User User { get; set; } = null!;
        public Hotel Hotel { get; set; } = null!;
    }
}