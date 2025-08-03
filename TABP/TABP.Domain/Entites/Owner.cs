using TABP.Domain.Common;
namespace TABP.Domain.Entites
{
    public class Owner : SoftDeletable
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public ICollection<Hotel> Hotels { get; set; } = [];
    }
}