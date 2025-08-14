using MediatR;
using TABP.Application.Common;
using TABP.Application.Rooms.Common;
namespace TABP.Application.Rooms.Queries.GetById
{
    public record GetRoomByIdQuery(long Id):IRequest<Result<RoomResponse>>;
}