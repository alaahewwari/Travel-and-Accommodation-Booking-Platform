using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    /// <summary>
    /// Entity Framework implementation of the user repository for user data access operations.
    /// Provides concrete implementation of user authentication, retrieval, and creation using Entity Framework Core.
    /// </summary>
    /// <param name="context">The Entity Framework database context for user operations.</param>
    /// <param name="hasher">The password hashing service for secure password verification.</param>
    public class UserRepository(ApplicationDbContext context, IPasswordHasher hasher) : IUserRepository
    {
        /// <inheritdoc />
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
        /// <inheritdoc />
        public async Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
            return user;
        }
        /// <inheritdoc />
        public async Task<User?> GetUserByIdAsync(long userId, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            return user;
        }
        /// <inheritdoc />
        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            return user;
        }
        /// <inheritdoc />
        public async Task<User?> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var createdUser = await context.Users.AddAsync(user, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return createdUser.Entity;
        }
    }
}