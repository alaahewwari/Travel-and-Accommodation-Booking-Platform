using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Models.Hotel;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    /// <summary>
    /// Entity Framework implementation of the room class repository for room class data access operations.
    /// Provides concrete implementation of room class CRUD operations, discount management, and amenity assignments using Entity Framework Core.
    /// </summary>
    /// <param name="context">The Entity Framework database context for room class operations.</param>
    public class RoomClassRepository(ApplicationDbContext context) : IRoomClassRepository
    {
        /// <inheritdoc />
        public async Task<Discount?> GetDiscountByRoomClassAsync(long roomClassId, CancellationToken cancellationToken)
        {
            return await context.RoomClasses
                    .AsNoTracking()
                    .Where(rc => rc.Id == roomClassId && rc.Discount != null && rc.Discount.EndDate > DateTime.UtcNow)
                    .Select(rc => rc.Discount)
                    .FirstOrDefaultAsync(cancellationToken);
        }
        /// <inheritdoc />
        public Task AssignAmenityToRoomClassAsync(Amenity amenity, RoomClass roomClass, CancellationToken cancellationToken)
        {
            amenity.RoomClasses.Add(roomClass);
            context.Amenities.Update(amenity);
            return context.SaveChangesAsync(cancellationToken);
        }
        /// <inheritdoc />
        public async Task<RoomClass> CreateRoomClassAsync(RoomClass roomClass, CancellationToken cancellationToken)
        {
            var created = await context.RoomClasses.AddAsync(roomClass, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return created.Entity;
        }
        /// <inheritdoc />
        public async Task<RoomClass?> GetRoomClassByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await context.RoomClasses
                .AsNoTracking()
                .FirstOrDefaultAsync(rc => rc.Id == id, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<RoomClass?> UpdateRoomClassAsync(RoomClass roomClass, CancellationToken cancellationToken)
        {
            var updated = await context.RoomClasses
                .Where(rc => rc.Id == roomClass.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(rc => rc.Description, roomClass.Description)
                    .SetProperty(rc => rc.AdultsCapacity, roomClass.AdultsCapacity)
                    .SetProperty(rc => rc.ChildrenCapacity, roomClass.ChildrenCapacity)
                    .SetProperty(rc => rc.UpdatedAt, DateTime.UtcNow)
                );
            if (updated == 0) return null;
            return await GetRoomClassByIdAsync(roomClass.Id, cancellationToken);
        }
        /// <inheritdoc />
        public async Task DeleteRoomClassAsync(RoomClass roomClass, CancellationToken cancellationToken)
        {
            context.RoomClasses.Remove(roomClass);
            await context.SaveChangesAsync(cancellationToken);
        }
        /// <inheritdoc />
        public async Task<IEnumerable<RoomClass>> GetAllRoomClassesAsync(CancellationToken cancellationToken)
        {
            var roomClasses = await context.RoomClasses
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return roomClasses;
        }
        /// <inheritdoc />
        public async Task<IEnumerable<FeaturedealsHotels>> GetFeaturedDealsInHotelsAsync(int count, CancellationToken cancellationToken)
        {
            var hotels = await context.RoomClasses
                .Where(rc => rc.Discount != null && rc.Discount.Percentage > 0)
                .Select(rc => new FeaturedealsHotels(
                rc.Hotel.Id,
                rc.Hotel.Name,
                rc.Hotel.Address,
                rc.Hotel.LocationLatitude,
                rc.Hotel.LocationLongitude,
                rc.Hotel.HotelImages
                    .Where(i => i.ImageType == ImageType.Thumbnail)
                    .Select(i => i.ImageUrl)
                    .FirstOrDefault()!,
                rc.PricePerNight,
                rc.PricePerNight - (rc.Discount!.Percentage / 100m * rc.PricePerNight)
                )).Take(count).AsNoTracking()
                .ToListAsync(cancellationToken);
            return hotels;
        }
    }
}