using TABP.Domain.Entities;
namespace TABP.Domain.Interfaces.Services
{
    public interface IJwtGenerator
    {
        string GenerateToken(User user);
    }
}