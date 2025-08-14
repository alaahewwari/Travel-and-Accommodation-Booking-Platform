using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using TABP.Application.Owners.Mapper;
using TABP.Domain.Common;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Models.Common;
using TABP.Domain.Models.Hotel;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    /// <summary>
    /// Entity Framework implementation of the hotel repository for hotel data access operations.
    /// Provides concrete implementation of hotel CRUD operations, advanced search with Sieve processing, and location-based queries using Entity Framework Core.
    /// </summary>
    /// <param name="context">The Entity Framework database context for hotel operations.</param>
    /// <param name="sieveProcessor">The Sieve processor for dynamic filtering, sorting, and pagination operations.</param>
    public class HotelRepository(ApplicationDbContext context, SieveProcessor sieveProcessor) : IHotelRepository
    {
        /// <inheritdoc />
        public async Task<Hotel> CreateHotelAsync(Hotel hotel, CancellationToken cancellationToken)
        {
            var createdHotel = await context.Hotels.AddAsync(hotel, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return createdHotel.Entity;
        }
        /// <inheritdoc />
        public async Task<Hotel?> GetHotelByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await context.Hotels
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<PagedResult<HotelForManagement>> GetAllHotelsAsync(SieveModel sieveModel, CancellationToken cancellationToken)
        {
            sieveModel.EnsureDefaults();
            var query = context.Hotels
                .Select(h => new HotelForManagement
                (
                    h.Id,
                    h.Name,
                    h.StarRating,
                    h.Owner.ToOwnerForManagement(),
                    h.RoomClasses.SelectMany(rc => rc.Rooms).Count(),
                    h.CreatedAt,
                    h.UpdatedAt
                ));
            var filteredQuery = sieveProcessor.Apply(sieveModel, query, applyPagination: false);
            var pagedQuery = sieveProcessor.Apply(sieveModel, filteredQuery, applyPagination: true);
            var items = await pagedQuery.ToListAsync(cancellationToken);
            var totalCount = await filteredQuery.CountAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling((double)totalCount / sieveModel.PageSize!.Value);
            var metadata = new PaginationMetadata(
                totalCount,
                totalPages,
                sieveModel.Page!.Value,
                sieveModel.PageSize.Value
            );
            return new PagedResult<HotelForManagement>
            {
                Items = items,
                PaginationMetadata = metadata
            };
        }
        /// <inheritdoc />
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
        /// <inheritdoc />
        public async Task<bool> DeleteHotelAsync(long id, CancellationToken cancellationToken)
        {
            var hotel = await GetHotelByIdAsync(id, cancellationToken);
            if (hotel is null)
                return false;
            context.Hotels.Remove(hotel);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        /// <inheritdoc />
        public async Task<bool> GetHotelByLocationAsync(double latitude, double longitude, CancellationToken cancellationToken)
        {
            return await context.Hotels
                .AnyAsync(h => h.LocationLatitude == latitude && h.LocationLongitude == longitude, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<PagedResult<HotelSearchResultResponse>> SearchAsync(HotelSearchParameters parameters, SieveModel sieveModel, CancellationToken cancellationToken)
        {
            sieveModel.EnsureDefaults();
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
            var totalPages = (int)Math.Ceiling((double)totalCount / sieveModel.PageSize.Value);
            if (sieveModel.Page.Value <= 1)
            {
                totalCount = await filteredQuery.CountAsync(cancellationToken);
                totalPages = (int)Math.Ceiling((double)totalCount / sieveModel.PageSize.Value);
            }
            var metadata = new PaginationMetadata
            (
                    totalCount,
                    totalPages,
                    sieveModel.Page.Value,
                    sieveModel.PageSize.Value
            );
            return new PagedResult<HotelSearchResultResponse>
            {
                Items = items,
                PaginationMetadata = metadata
            };
        }
        /// <inheritdoc />
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