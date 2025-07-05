using TABP.Domain.Common;
namespace TABP.Domain.Entites
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