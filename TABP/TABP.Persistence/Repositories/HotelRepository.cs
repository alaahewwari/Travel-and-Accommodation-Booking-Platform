using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entites;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Models;
namespace TABP.Persistence.Repositories
{
    public class HotelRepository(ApplicationDbContext context) : IHotelRepository
    {
        public async Task<Hotel> CreateHotelAsync(Hotel hotel, CancellationToken cancellationToken)
        {
            var createdHotel = await context.Hotels.AddAsync(hotel, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return createdHotel.Entity;
        }
        public async Task<Hotel?> GetHotelByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await context.Hotels
                .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
        }
        public async Task<IEnumerable<HotelForManagement>> GetAllHotelsAsync(CancellationToken cancellationToken)
        {
            var hotels = await context.Hotels
                .Select(h => new HotelForManagement
                (
                    h.Id,
                    h.Name,
                    h.StarRating,
                    h.Owner,
                    h.RoomClasses.SelectMany(rc => rc.Rooms).Count(),
                    h.CreatedAt,
                    h.UpdatedAt
                )).ToListAsync(cancellationToken);
            return hotels;
        }
        public async Task<Hotel?> UpdateHotelAsync(Hotel hotel, CancellationToken cancellationToken)
        {
            var updatedCount = await context.Hotels
                .Where(h => h.Id == hotel.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(h => h.Name, hotel.Name)
                    .SetProperty(h => h.Description, hotel.Description)
                    .SetProperty(h => h.BriefDescription, hotel.BriefDescription)
                    .SetProperty(h => h.Address, hotel.Address)
                    .SetProperty(h => h.StarRating, hotel.StarRating)
                    .SetProperty(h => h.LocationLatitude, hotel.LocationLatitude)
                    .SetProperty(h => h.LocationLongitude, hotel.LocationLongitude)
                    .SetProperty(h => h.UpdatedAt, DateTime.UtcNow)
                );
            if (updatedCount == 0)
                return null;
            return await GetHotelByIdAsync(hotel.Id, cancellationToken);
        }
        public async Task<bool> DeleteHotelAsync(long id, CancellationToken cancellationToken)
        {
            var hotel = await GetHotelByIdAsync(id, cancellationToken);
            if (hotel is null)
                return false;
            context.Hotels.Remove(hotel);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> GetHotelByLocationAsync(double latitude, double longitude, CancellationToken cancellationToken)
        {
            return await context.Hotels
                .AnyAsync(h => h.LocationLatitude == latitude && h.LocationLongitude == longitude, cancellationToken);
        }
    }
}