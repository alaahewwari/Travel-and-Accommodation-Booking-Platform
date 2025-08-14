using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.RoomClasses;
using TABP.Application.RoomClasses.Commands.Create;
using TABP.Application.RoomClasses.Commands.Update;
namespace TABP.API.Mappers
{
    [Mapper]
    public static partial class RoomClassMapper
    {
        public static partial CreateRoomClassCommand ToCommand(this CreateRoomClassRequest request);
        public static partial UpdateRoomClassCommand ToCommand(this UpdateRoomClassRequest request, long id);
    }
}