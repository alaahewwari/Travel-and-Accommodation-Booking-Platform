using MediatR;
using TABP.Application.Common;
using TABP.Application.Rooms.Common;
namespace TABP.Application.Rooms.Commands.Create
{
    public record CreateRoomCommand(long HotelId, long RoomClassId, string Number):IRequest<Result<RoomResponse>>;
}