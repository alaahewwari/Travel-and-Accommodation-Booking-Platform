using MediatR;
using TABP.Application.Common;
namespace TABP.Application.RoomClasses.Commands.Delete
{
    public record DeleteRoomClassCommand(long Id) : IRequest<Result>;
}