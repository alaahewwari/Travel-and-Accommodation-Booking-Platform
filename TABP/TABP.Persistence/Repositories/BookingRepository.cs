using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Models;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    public class BookingRepository(
        ApplicationDbContext context,
        SieveProcessor sieveProcessor
        ) : IBookingRepository
    {
        public async Task<Booking> CreateAsync(Booking booking, CancellationToken cancellationToken)
        {
            var createdBooking = await context.AddAsync(booking);
            return createdBooking.Entity;
        }
        public async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            var booking = await GetByIdAsync(id, cancellationToken);
            booking.Status = BookingStatus.Cancelled;
            await context.SaveChangesAsync(cancellationToken);
        }
        public async Task<PagedResult<Booking>> GetBookingsAsync(long userId, SieveModel sieveModel, CancellationToken cancellationToken)
        {
            var query = context.Bookings.AsQueryable();
            var filteredQuery = sieveProcessor.Apply(sieveModel, query, applyPagination: false);
            var pagedQuery = sieveProcessor.Apply(sieveModel, query, applyPagination: true);
            var items = await query
                .Include(b=>b.Hotel)
                .AsNoTracking()
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
            return new PagedResult<Booking>
            {
                Items = items,
                PaginationMetadata = metadata
            };
        }
        public Task<Booking?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            var booking = context.Bookings
                .Include(b => b.Rooms)
                .Include(b => b.Hotel)
                .Include(b => b.User)
                .Include(b=> b.Invoice)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
            return booking;
        }
        public async Task<bool> CheckBookingOverlapAsync(
            IEnumerable<long> roomIds,
            DateTime checkIn,
            DateTime checkOut,
            CancellationToken cancellationToken)
            {
            return await context.Bookings
                .Where(b =>
                    b.Status != BookingStatus.Cancelled &&
                    b.Status != BookingStatus.Completed &&
                    b.CheckInDate <= checkOut &&
                    b.CheckOutDate >= checkIn &&
                    b.Rooms.Any(r => roomIds.Contains(r.Id))
                )
                .AnyAsync(cancellationToken);
        }
    }
}