using TABP.Domain.Common;
namespace TABP.Domain.Entites
{
    public class Role : SoftDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}