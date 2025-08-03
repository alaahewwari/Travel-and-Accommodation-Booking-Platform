using TABP.Domain.Common;
namespace TABP.Domain.Entites
{
    public class Room: SoftDeletable
    {
        public long Id { get; set; }
        public string Number { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long RoomClassId { get; set; }
        public RoomClass RoomClass { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = [];
    }
}