using Riok.Mapperly.Abstractions;
using TABP.Application.Rooms.Commands.Create;
using TABP.Application.Rooms.Commands.Update;
using TABP.Application.Rooms.Common;
using TABP.Domain.Entites;
using TABP.Domain.Models;
namespace TABP.Application.Rooms.Mapper
{
    [Mapper]
    public static partial class RoomMapper
    {
        public static partial RoomForManagementResponse ToRoomForManagementResponse(this RoomForManagement Room);
        public static Room ToRoomDomain(this CreateRoomCommand command)
        {
            var room = ToRoomDomainInternal(command);
            room.CreatedAt = DateTime.UtcNow;
            room.UpdatedAt = DateTime.UtcNow;
            return room;
        }
        public static Room ToRoomDomain(this UpdateRoomCommand command)
        {
            var Room = ToRoomDomainInternal(command);
            Room.UpdatedAt = DateTime.UtcNow;
            return Room;
        }
        public static partial RoomResponse ToRoomResponse(this Room room);
        private static partial Room ToRoomDomainInternal(CreateRoomCommand command);
        private static partial Room ToRoomDomainInternal(UpdateRoomCommand command);
    }
}
