using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.Rooms;
using TABP.Application.Rooms.Commands.Create;
using TABP.Application.Rooms.Commands.Update;
namespace TABP.API.Mapping
{
    [Mapper]
    public static partial class RoomMapping
    {
        public static partial CreateRoomCommand ToCommand(this CreateRoomRequest request, long hotelId, long roomClassId);
        public static partial UpdateRoomCommand ToCommand(this UpdateRoomRequest request, long id);
    }
}