using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    public class RoleRepository(ApplicationDbContext context) : IRoleRepository
    {
        public async Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken token)
        {
            return await context.Roles
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Role> CreateRoleAsync(Role role, CancellationToken cancellationToken)
        {
            await context.Roles.AddAsync(role);
            await context.SaveChangesAsync(cancellationToken);
            return role;
        }
        public async Task<bool> DeleteRoleAsync(long id, CancellationToken cancellationToken)
        {
            var role = await context.Roles.FindAsync(id);
            if (role != null)
            {
                context.Roles.Remove(role);
                await context.SaveChangesAsync(cancellationToken);
            }
            return role is not null;
        }
        public async Task<Role?> GetRoleByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<Role?> UpdateRoleAsync(Role role, CancellationToken cancellationToken)
        {
            var existing = GetRoleByIdAsync(role.Id, cancellationToken);
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
        public async Task<Role?> GetRoleByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
        }
    }
}