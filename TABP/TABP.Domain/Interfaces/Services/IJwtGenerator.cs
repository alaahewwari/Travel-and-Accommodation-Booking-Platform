using TABP.Domain.Entites;
namespace TABP.Domain.Interfaces.Services
{
    public interface IJwtGenerator
    {
        string GenerateToken(User user);
    }
}