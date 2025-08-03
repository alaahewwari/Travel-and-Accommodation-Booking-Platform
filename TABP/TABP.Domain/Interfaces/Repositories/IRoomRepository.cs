using TABP.Domain.Entities;
using TABP.Domain.Models.Room;
namespace TABP.Domain.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        Task<Room> CreateRoomAsync(Room room, CancellationToken cancellationToken);
        Task<Room?> GetRoomByIdAsync(long id, CancellationToken cancellationToken);
        Task<Room?> GetRoomWithClassByIdAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<RoomForManagement>> GetAllRoomsAsync(CancellationToken cancellationToken);
        Task<Room?> UpdateRoomAsync(Room room, CancellationToken cancellationToken);
        Task<bool> DeleteRoomAsync(long id, CancellationToken cancellationToken);
        Task<Room?> GetRoomByHotelAsync(Hotel hotel, CancellationToken cancellationToken);
        Task<IEnumerable<Room>> GetRoomsByIdsAsync(IEnumerable<long> roomIds, long HotelId, CancellationToken cancellationToken);
    }
}