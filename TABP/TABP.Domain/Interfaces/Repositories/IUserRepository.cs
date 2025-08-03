using TABP.Domain.Entites;
namespace TABP.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> AuthenticateUserAsync(string username, string password, CancellationToken cancellationToken);
        Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
        Task<User?> GetUserByIdAsync(long userId, CancellationToken cancellationToken);
    }
}