using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Models;
using TABP.Domain.Models.Hotel;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    public class HotelRepository(ApplicationDbContext context, SieveProcessor sieveProcessor) : IHotelRepository
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
                .AsNoTracking()
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
        public async Task<PagedResult<HotelSearchResultResponse>> SearchAsync(HotelSearchParameters parameters, SieveModel sieveModel, CancellationToken cancellationToken)
        {
            var query = context.Hotels.AsQueryable();
            query = query
                .Where(h =>
                        h.RoomClasses.Any(rc =>
                        rc.AdultsCapacity >= parameters.Adults &&
                        rc.ChildrenCapacity >= parameters.Children));
            var filteredQuery = sieveProcessor.Apply(sieveModel, query, applyPagination: false);
            var pagedQuery = sieveProcessor.Apply(sieveModel, query, applyPagination: true);
            var items = await pagedQuery
                .Select(h => new HotelSearchResultResponse(
                    h.Id,
                    h.Name,
                    h.StarRating,
                    h.Reviews,
                    h.BriefDescription,
                    h.HotelImages
                        .Where(i => i.ImageType == ImageType.Thumbnail)
            .Select(i => i.ImageUrl)
                        .FirstOrDefault()!,
                    h.RoomClasses.Any() ? h.RoomClasses.Min(rc => rc.PricePerNight) : 0
                ))
                .ToListAsync(cancellationToken);
            var totalCount = await pagedQuery.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / sieveModel.PageSize.GetValueOrDefault(10));
            if (sieveModel.Page.GetValueOrDefault() <= 1)
            {
                totalCount = await filteredQuery.CountAsync(cancellationToken);
                totalPages = (int)Math.Ceiling((double)totalCount / sieveModel.PageSize.GetValueOrDefault(10));
            }
            var metadata = new PaginationMetadata
            (
                    totalCount,
                    totalPages,
                    sieveModel.Page.GetValueOrDefault(1),
                    sieveModel.PageSize.GetValueOrDefault(10)
            );
            return new PagedResult<HotelSearchResultResponse>
            {
                Items = items,
                PaginationMetadata = metadata
            };
        }
        public async Task<IEnumerable<Hotel>> GetRecentlyVisitedHotelsAsync(long userId, int count, CancellationToken cancellationToken)
        {
            return await context.Bookings
                .Where(b => b.UserId == userId && b.Status == BookingStatus.Completed)
                .OrderByDescending(b => b.CheckOutDate)
                .Select(b => b.Hotel)
                .Distinct()
                .Take(count)
                .ToListAsync(cancellationToken);
        }
    }
}