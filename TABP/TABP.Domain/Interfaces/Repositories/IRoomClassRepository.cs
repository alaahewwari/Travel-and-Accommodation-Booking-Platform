using TABP.Domain.Entites;
using TABP.Domain.Models;
namespace TABP.Domain.Interfaces.Repositories
{
    public interface IRoomClassRepository
    {
        Task<RoomClass> CreateRoomClassAsync(RoomClass roomClass, CancellationToken cancellationToken);
        Task<RoomClass?> GetRoomClassByIdAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<RoomClass>> GetAllRoomClassesAsync(CancellationToken cancellationToken);
        Task<RoomClass?> UpdateRoomClassAsync(RoomClass roomClass, CancellationToken cancellationToken);
        Task DeleteRoomClassAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<FeaturedealsHotels>> GetFeaturedDealsInHotelsAsync(int count, CancellationToken cancellationToken);
    }
}