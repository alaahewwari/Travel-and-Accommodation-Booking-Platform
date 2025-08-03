using TABP.Domain.Entites;
using TABP.Domain.Models;
namespace TABP.Domain.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        Task<Room> CreateRoomAsync(Room room, CancellationToken cancellationToken);
        Task<Room?> GetRoomByIdAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<RoomForManagement>> GetAllRoomsAsync(CancellationToken cancellationToken);
        Task<Room?> UpdateRoomAsync(Room room, CancellationToken cancellationToken);
        Task<bool> DeleteRoomAsync(long id, CancellationToken cancellationToken);
        Task<Room?> GetRoomByHotelAsync(Hotel hotel, CancellationToken cancellationToken);
    }
}