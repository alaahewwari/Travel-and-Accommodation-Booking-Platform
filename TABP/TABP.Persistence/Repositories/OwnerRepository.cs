using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    /// <summary>
    /// Entity Framework implementation of the owner repository for hotel owner data access operations.
    /// Provides concrete implementation of owner CRUD operations and owner management using Entity Framework Core.
    /// </summary>
    /// <param name="context">The Entity Framework database context for owner operations.</param>
    public class OwnerRepository(ApplicationDbContext context) : IOwnerRepository
    {
        /// <inheritdoc />
        public async Task<Owner> CreateOwnerAsync(Owner owner, CancellationToken cancellationToken)
        {
            var created = await context.Owners.AddAsync(owner, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return created.Entity;
        }
        /// <inheritdoc />
        public async Task<Owner?> GetOwnerByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await context.Owners
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<IEnumerable<Owner>> GetAllOwnersAsync(CancellationToken cancellationToken)
        {
            return await context.Owners
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        /// <inheritdoc />
        public async Task<Owner?> UpdateOwnerAsync(Owner owner, CancellationToken cancellationToken)
        {
            var updated = await context.Owners
                .Where(o => o.Id == owner.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(o => o.FirstName, owner.FirstName)
                    .SetProperty(o => o.LastName, owner.LastName)
                    .SetProperty(o => o.PhoneNumber, owner.PhoneNumber));
            if (updated == 0) return null;
            return await GetOwnerByIdAsync(owner.Id, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<bool> DeleteOwnerAsync(long id, CancellationToken cancellationToken)
        {
            var owner = await GetOwnerByIdAsync(id, cancellationToken);
            if (owner is null) return false;
            context.Owners.Remove(owner);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}