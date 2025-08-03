using TABP.Domain.Common;
namespace TABP.Domain.Entites
{
    public class User : SoftDeletable
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}