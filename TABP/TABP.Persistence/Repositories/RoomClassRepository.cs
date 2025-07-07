using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entites;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Models;
namespace TABP.Persistence.Repositories
{
    public class RoomClassRepository(ApplicationDbContext context) : IRoomClassRepository
    {
        public async Task<RoomClass> CreateRoomClassAsync(RoomClass roomClass, CancellationToken cancellationToken)
        {
            var created = await context.RoomClasses.AddAsync(roomClass, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return created.Entity;
        }
        public async Task<RoomClass?> GetRoomClassByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await context.RoomClasses
                .AsNoTracking()
                .FirstOrDefaultAsync(rc => rc.Id == id, cancellationToken);
        }
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
        public async Task DeleteRoomClassAsync(long id, CancellationToken cancellationToken)
        {
            var roomClass = await GetRoomClassByIdAsync(id, cancellationToken);
            context.RoomClasses.Remove(roomClass);
            await context.SaveChangesAsync(cancellationToken);
        }
        public async Task<IEnumerable<RoomClass>> GetAllRoomClassesAsync(CancellationToken cancellationToken)
        {
            var roomClasses = await context.RoomClasses
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return roomClasses;
        }
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