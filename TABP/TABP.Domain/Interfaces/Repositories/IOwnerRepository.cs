using TABP.Domain.Entities;
namespace TABP.Domain.Interfaces.Repositories
{
    public interface IOwnerRepository
    {
        Task<Owner> CreateOwnerAsync(Owner owner, CancellationToken cancellationToken);
        Task<Owner?> GetOwnerByIdAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<Owner>> GetAllOwnersAsync(CancellationToken cancellationToken);
        Task<Owner?> UpdateOwnerAsync(Owner owner, CancellationToken cancellationToken);
        Task<bool> DeleteOwnerAsync(long id, CancellationToken cancellationToken);
    }
}