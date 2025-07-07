using MediatR;
using TABP.Application.Common;
using TABP.Application.Rooms.Common;
namespace TABP.Application.Rooms.Commands.Update
{
    public record UpdateRoomCommand(long Id, string Number): IRequest<Result<RoomResponse>>;
}