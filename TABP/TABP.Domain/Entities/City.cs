using TABP.Domain.Common;
namespace TABP.Domain.Entities
{
    public class City : SoftDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string PostOffice { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Hotel> Hotels { get; set; } = [];
        public ICollection<CityImage> CityImages { get; set; } = [];
    }
}