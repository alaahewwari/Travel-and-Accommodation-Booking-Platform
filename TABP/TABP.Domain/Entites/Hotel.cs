using TABP.Domain.Common;
namespace TABP.Domain.Entites
{
    public class Hotel : SoftDeletable
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string BriefDescription { get; set; } = null!;
        public string Address { get; set; } = null!;
        public byte StarRating { get; set; }
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public City City { get; set; } = null!;
    }
}