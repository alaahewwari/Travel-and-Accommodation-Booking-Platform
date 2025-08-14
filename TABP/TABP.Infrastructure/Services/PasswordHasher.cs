using System.Security.Cryptography;
using System.Text;
using TABP.Domain.Interfaces.Services;
namespace TABP.Infrastructure.Services
{
    public class PasswordHasher: IPasswordHasher
    {
        public (string Hash, string Salt) HashPassword(string password)
        {
            var saltBytes = RandomNumberGenerator.GetBytes(16);
            var salt = Convert.ToBase64String(saltBytes);
            using var sha256 = SHA256.Create();
            var combined = Encoding.UTF8.GetBytes(salt + password);
            var hash = sha256.ComputeHash(combined);
            return (Convert.ToBase64String(hash), salt);
        }
        public bool VerifyPassword(string password, string hash, string salt)
        {
            using var sha256 = SHA256.Create();
            var combined = Encoding.UTF8.GetBytes(salt + password);
            var computedHash = sha256.ComputeHash(combined);
            return (Convert.ToBase64String(computedHash) == hash);
        }
    }
}