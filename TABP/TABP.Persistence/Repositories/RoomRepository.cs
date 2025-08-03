using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Models.Room;
using TABP.Persistence.Context;
namespace TABP.Persistence.Repositories
{
    /// <summary>
    /// Entity Framework implementation of the room repository for room data access operations.
    /// Provides concrete implementation of room CRUD operations and hotel-specific room queries using Entity Framework Core.
    /// </summary>
    /// <param name="context">The Entity Framework database context for room operations.</param>
    public class RoomRepository(ApplicationDbContext context) : IRoomRepository
    {
        /// <inheritdoc />
        public async Task<Room> CreateRoomAsync(Room room, CancellationToken cancellationToken)
        {
            var result = await context.Rooms.AddAsync(room, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }

        /// <inheritdoc />
        public async Task<Room?> GetRoomByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await context.Rooms
                .Include(r => r.RoomClass)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<RoomForManagement>> GetAllRoomsAsync(CancellationToken cancellationToken)
        {
            var rooms = await context.Rooms
                .Include(r => r.RoomClass)
                .Select(r => new RoomForManagement
                (
                    r.Id,
                    r.Number,
                    true,//placeholder
                    r.RoomClass.AdultsCapacity,
                    r.RoomClass.ChildrenCapacity,
                    r.CreatedAt,
                    r.UpdatedAt
                )).ToListAsync(cancellationToken);
            return rooms;
        }

        /// <inheritdoc />
        public async Task<Room?> UpdateRoomAsync(Room room, CancellationToken cancellationToken)
        {
            var affected = await context.Rooms
                .Where(r => r.Id == room.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(r => r.Number, room.Number)
                    .SetProperty(r => r.UpdatedAt, DateTime.UtcNow),
                    cancellationToken);
            if (affected == 0)
                return null;
            return await GetRoomByIdAsync(room.Id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteRoomAsync(long id, CancellationToken cancellationToken)
        {
            var room = await GetRoomByIdAsync(id, cancellationToken);
            if (room is null)
                return false;
            context.Rooms.Remove(room);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <inheritdoc />
        public async Task<Room?> GetRoomByHotelAsync(Hotel hotel, CancellationToken cancellationToken)
        {
            var room = await context.Rooms.Where(r => r.RoomClass.Hotel == hotel)
                .FirstOrDefaultAsync(cancellationToken);
            return room;
        }

        /// <inheritdoc />
        public async Task<Room?> GetRoomWithClassByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await context.Rooms
                .Include(r => r.RoomClass)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Room>> GetRoomsByIdsAsync(IEnumerable<long> roomIds, long HotelId, CancellationToken cancellationToken)
        {
            var idList = roomIds?.Distinct().ToList() ?? new List<long>();
            if (!idList.Any())
                return [];
            return await context.Rooms
                .Include(r => r.RoomClass)
                .Where(r => idList.Contains(r.Id) && r.HotelId == HotelId)
                .ToListAsync(cancellationToken);
        }
    }
}