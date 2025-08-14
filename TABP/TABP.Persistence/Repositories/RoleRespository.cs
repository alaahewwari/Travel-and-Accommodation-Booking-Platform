using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    /// <summary>
    /// Entity Framework implementation of the role repository for role data access operations.
    /// Provides concrete implementation of role CRUD operations and role-based queries using Entity Framework Core.
    /// </summary>
    /// <param name="context">The Entity Framework database context for role operations.</param>
    public class RoleRepository(ApplicationDbContext context) : IRoleRepository
    {
        /// <inheritdoc />
        public async Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            return await context.Roles
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        /// <inheritdoc />
        public async Task<Role> CreateRoleAsync(Role role, CancellationToken cancellationToken)
        {
            await context.Roles.AddAsync(role, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return role;
        }
        /// <inheritdoc />
        public async Task<bool> DeleteRoleAsync(long id, CancellationToken cancellationToken)
        {
            var role = await context.Roles.FindAsync([id], cancellationToken);
            if (role != null)
            {
                context.Roles.Remove(role);
                await context.SaveChangesAsync(cancellationToken);
            }
            return role is not null;
        }
        /// <inheritdoc />
        public async Task<Role?> GetRoleByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<Role?> UpdateRoleAsync(Role role, CancellationToken cancellationToken)
        {
            var existing = await GetRoleByIdAsync(role.Id, cancellationToken);
            if (existing is null)
            {
                return null;
            }
            var updatedCount = await context.Roles
                .Where(r => r.Id == role.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(r => r.Name, role.Name), cancellationToken);
            if (updatedCount == 0)
                return null;
            return await GetRoleByIdAsync(role.Id, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<Role?> GetRoleByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
        }
    }
}