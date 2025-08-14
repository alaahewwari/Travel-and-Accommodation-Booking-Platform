using TABP.Domain.Entities.Common;
namespace TABP.Domain.Entities
{
    public class Discount: SoftDeletable
    {
        public int Id { get; set; }
        public int Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<RoomClass> RoomClasses { get; set; } = [];
    }
}