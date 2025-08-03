using Sieve.Models;
using TABP.Domain.Entities;
using TABP.Domain.Models;
namespace TABP.Domain.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> CreateAsync(Booking booking, CancellationToken cancellationToken);
        Task DeleteAsync(long id, CancellationToken cancellationToken);
        Task<Booking?> GetByIdAsync(long id, CancellationToken cancellationToken);
        Task<PagedResult<Booking>> GetBookingsAsync(long userId,SieveModel sieveModel, CancellationToken cancellationToken);
        Task<IEnumerable<Booking>> GetRecentBookingsInDifferentHotelsByGuestId(long guestId, int count, CancellationToken cancellationToken);
        Task<bool> CheckBookingOverlapAsync(IEnumerable<long> roomIds, DateTime checkInDate, DateTime checkOutDate, CancellationToken cancellationToken);
    }
}