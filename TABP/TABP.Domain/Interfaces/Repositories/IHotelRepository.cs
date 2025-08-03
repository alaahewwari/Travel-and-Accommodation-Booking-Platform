using Sieve.Models;
using TABP.Domain.Entities;
using TABP.Domain.Models;
using TABP.Domain.Models.Hotel;
namespace TABP.Domain.Interfaces.Repositories
{
    public interface IHotelRepository
    {
        Task<Hotel> CreateHotelAsync(Hotel hotel, CancellationToken cancellationToken);
        Task<Hotel?> GetHotelByIdAsync(long hotelId, CancellationToken cancellationToken);
        Task<IEnumerable<HotelForManagement>> GetAllHotelsAsync(CancellationToken cancellationToken);
        Task<Hotel?> UpdateHotelAsync(Hotel hotel, CancellationToken cancellationToken);
        Task<bool> DeleteHotelAsync(long hotelId, CancellationToken cancellationToken);
        Task<bool> GetHotelByLocationAsync(double latitude, double longitude, CancellationToken cancellationToken);
        Task<PagedResult<HotelSearchResultResponse>> SearchAsync(HotelSearchParameters parameters,SieveModel sieveModel, CancellationToken cancellationToken);
    }
}