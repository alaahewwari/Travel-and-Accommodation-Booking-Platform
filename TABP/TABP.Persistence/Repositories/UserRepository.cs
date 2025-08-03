using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext context, IPasswordHasher hasher) : IUserRepository
    {
        public async Task<User?> AuthenticateUserAsync(string username, string password, CancellationToken cancellationToken)
        {
            var user = await GetUserByUsernameAsync(username, cancellationToken);
            if (user == null)
            {
                return null;
            }
            var isPasswordValid = hasher.VerifyPassword(password, user.PasswordHash, user.Salt);
            if (!isPasswordValid)
            {
                return null;
            }
            return user;
        }
        public async Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
            return user;
        }
        public async Task<User?> GetUserByIdAsync(long userId, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            return user;
        }
        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            return user;
        }
        public async Task<User?> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var createdUser = await context.Users.AddAsync(user, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return createdUser.Entity;
        }
    }
}