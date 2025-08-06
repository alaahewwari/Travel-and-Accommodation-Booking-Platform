using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    /// <summary>
    /// Entity Framework implementation of the amenity repository for amenity data access operations.
    /// Provides concrete implementation of amenity CRUD operations, name-based lookups, and soft delete functionality using Entity Framework Core.
    /// </summary>
    /// <param name="context">The Entity Framework database context for amenity operations.</param>
    public class AmenityRepository(ApplicationDbContext context) : IAmenityRepository
    {
        /// <inheritdoc />
        public async Task<Amenity> CreateAmenityAsync(Amenity amenity, CancellationToken cancellationToken)
        {
            var createdAmenity = await context.Amenities.AddAsync(amenity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return createdAmenity.Entity;
        }
        /// <inheritdoc />
        public async Task<IEnumerable<Amenity>> GetAllAmenitiesAsync(CancellationToken cancellationToken)
        {
            return await context.Amenities
                .AsNoTracking()
                .Where(a => !a.IsDeleted)
                .ToListAsync(cancellationToken);
        }
        /// <inheritdoc />
        public async Task<Amenity?> GetAmenityByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await context.Amenities
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<Amenity?> GetAmenityByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await context.Amenities
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Name == name, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<Amenity?> UpdateAmenityAsync(Amenity amenity, CancellationToken cancellationToken)
        {
            var updatedCount = await context.Amenities
                .Where(a => a.Id == amenity.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(a => a.Name, amenity.Name)
                    .SetProperty(a => a.Description, amenity.Description)
                    .SetProperty(a => a.UpdatedAt, DateTime.UtcNow),
                    cancellationToken);
            if (updatedCount == 0)
                return null;
            return await GetAmenityByIdAsync(amenity.Id, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<bool> SoftDeleteAmenityAsync(long id, CancellationToken cancellationToken)
        {
            var updatedCount = await context.Amenities
                .Where(a => a.Id == id && !a.IsDeleted)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(a => a.IsDeleted, true)
                    .SetProperty(a => a.UpdatedAt, DateTime.UtcNow),
                    cancellationToken);
            return updatedCount > 0;
        }
    }
}