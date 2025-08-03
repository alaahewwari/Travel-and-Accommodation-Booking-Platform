using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Models.Common;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    /// <summary>
    /// Entity Framework implementation of the booking repository for booking data access operations.
    /// Provides concrete implementation of booking CRUD operations, advanced filtering with Sieve processing, and availability checking using Entity Framework Core.
    /// </summary>
    /// <param name="context">The Entity Framework database context for booking operations.</param>
    /// <param name="sieveProcessor">The Sieve processor for dynamic filtering, sorting, and pagination operations.</param>
    public class BookingRepository(
        ApplicationDbContext context,
        SieveProcessor sieveProcessor
        ) : IBookingRepository
    {
        /// <inheritdoc />
        public async Task<Booking> CreateAsync(Booking booking, CancellationToken cancellationToken)
        {
            var createdBooking = await context.Bookings.AddAsync(booking, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return createdBooking.Entity;
        }
        /// <inheritdoc />
        public async Task CancelAsync(Booking booking, CancellationToken cancellationToken)
        {
            booking.Status = BookingStatus.Cancelled;
            await context.SaveChangesAsync(cancellationToken);
        }
        /// <inheritdoc />
        public async Task<PagedResult<Booking>> GetBookingsAsync(long userId, SieveModel sieveModel, CancellationToken cancellationToken)
        {
            var query = context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Hotel);
            var filteredQuery = sieveProcessor.Apply(sieveModel, query, applyPagination: false);
            var pagedQuery = sieveProcessor.Apply(sieveModel, filteredQuery, applyPagination: true);
            var items = await pagedQuery
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            var totalCount = await filteredQuery.CountAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling((double)totalCount / sieveModel.PageSize.GetValueOrDefault(10));
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
        /// <inheritdoc />
        public Task<Booking?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return context.Bookings
                .Include(b => b.Rooms)
                .Include(b => b.Hotel)
                .Include(b => b.User)
                .Include(b => b.Invoice)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }
        /// <inheritdoc />
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
                    b.CheckInDate < checkOut &&
                    b.CheckOutDate > checkIn &&
                    b.Rooms.Any(r => roomIds.Contains(r.Id))
                )
                .AnyAsync(cancellationToken);
        }
        /// <inheritdoc />
        public async Task UpdateBookingAsync(Booking booking, CancellationToken cancellationToken)
        {
            context.Bookings.Update(booking);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}