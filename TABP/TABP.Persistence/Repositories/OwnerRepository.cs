using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entites;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Persistence.Repositories
{
    public class OwnerRepository(ApplicationDbContext context) : IOwnerRepository
    {
        public async Task<Owner> CreateOwnerAsync(Owner owner, CancellationToken cancellationToken)
        {
            var createdOwner = await context.Owners.AddAsync(owner, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return createdOwner.Entity;
        }
        public async Task<Owner?> GetOwnerByIdAsync(long id, CancellationToken cancellationToken)
        {
            var owner = await context.Owners
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
            return owner;
        }
        public async Task<IEnumerable<Owner>> GetAllOwnersAsync(CancellationToken cancellationToken)
        {
            var owners = await context.Owners
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return owners;
        }
        public async Task<Owner?> UpdateOwnerAsync(Owner owner, CancellationToken cancellationToken)
        {
            var updatedOwner = await context.Owners
                .Where(o => o.Id == owner.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(o => o.FirstName, owner.FirstName)
                .SetProperty(o => o.LastName, owner.LastName)
                .SetProperty(o => o.PhoneNumber, owner.PhoneNumber)
            );
            await context.SaveChangesAsync(cancellationToken);
            if (updatedOwner == 0)
            {
                return null;
            }
            return await GetOwnerByIdAsync(owner.Id, cancellationToken);
        }
        public async Task<bool> DeleteOwnerAsync(long id, CancellationToken cancellationToken)
        {
            var owner = await GetOwnerByIdAsync(id, cancellationToken);
            if (owner == null)
            {
                return false;
            }
            context.Owners.Remove(owner);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}