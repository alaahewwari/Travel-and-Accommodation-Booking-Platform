namespace TABP.Domain.Entites
{
    public class CartItem
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public long RoomId { get; set; }
        public User User { get; set; }= null!;
        public Room Room { get; set; }= null!;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}