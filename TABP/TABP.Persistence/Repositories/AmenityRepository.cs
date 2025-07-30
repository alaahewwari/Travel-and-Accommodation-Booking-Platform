using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    public class AmenityRepository(ApplicationDbContext context) : IAmenityRepository
    {
        public Task AssignAmenityToRoomClassAsync(Amenity amenity, RoomClass roomClass, CancellationToken cancellationToken)
        {
            amenity.RoomClasses.Add(roomClass);
            context.Amenities.Update(amenity);
            return context.SaveChangesAsync(cancellationToken);
        }
        public async Task<Amenity?> CreateAmenityAsync(Amenity amenity, CancellationToken cancellationToken)
        {
            var createdAmenity = await context.Amenities.AddAsync(amenity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return createdAmenity.Entity;
        }
        public async Task<IEnumerable<Amenity>> GetAllAmenitiesAsync(CancellationToken cancellationToken)
        {
            return await context.Amenities
                .AsNoTracking()
                .Where(a => !a.IsDeleted)
                .ToListAsync(cancellationToken);
        }
        public async Task<Amenity?> GetAmenityByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await context.Amenities
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
        public async Task<Amenity?> GetAmenityByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await context.Amenities
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Name == name, cancellationToken);
        }
        public async Task<Amenity?> UpdateAmenityAsync(Amenity amenity, CancellationToken cancellationToken)
        {
            var updatedCount = await context.Amenities
                .Where(a => a.Id == amenity.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(a => a.Name, amenity.Name)
                    .SetProperty(a => a.Description, amenity.Description),
                    cancellationToken);
            if (updatedCount == 0)
                return null;
            return await GetAmenityByIdAsync(amenity.Id, cancellationToken);
        }
    }
}