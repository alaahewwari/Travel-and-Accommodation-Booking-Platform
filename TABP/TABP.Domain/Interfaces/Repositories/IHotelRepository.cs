using TABP.Domain.Entites;
namespace TABP.Domain.Interfaces.Repositories
{
    public interface IHotelRepository
    {
        Task<Hotel> CreateHotelAsync(Hotel hotel, CancellationToken cancellationToken);
        Task<Hotel?> GetHotelByIdAsync(long hotelId, CancellationToken cancellationToken);
        Task<IEnumerable<Hotel>> GetAllHotelsAsync(CancellationToken cancellationToken);
        Task<Hotel?> UpdateHotelAsync(Hotel hotel, CancellationToken cancellationToken);
        Task<bool> DeleteHotelAsync(long hotelId, CancellationToken cancellationToken);
        Task<bool> GetHotelByLocationAsync(double latitude, double longitude, CancellationToken cancellationToken);
    }
}