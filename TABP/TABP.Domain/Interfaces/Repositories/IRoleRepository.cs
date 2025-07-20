using TABP.Domain.Entities;
namespace TABP.Domain.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken token);
        Task<Role?> GetRoleByIdAsync(int id, CancellationToken cancellationToken);
        Task<Role> CreateRoleAsync(Role role, CancellationToken cancellationToken);
        Task<Role?> UpdateRoleAsync(Role role, CancellationToken cancellationToken);
        Task<bool> DeleteRoleAsync(long id, CancellationToken cancellationToken);
        Task<Role> GetRoleByNameAsync(string name, CancellationToken cancellationToken);
    }
}