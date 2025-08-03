using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Rooms.Commands.Delete
{
    public record DeleteRoomCommand(long Id) : IRequest<Result>;
}