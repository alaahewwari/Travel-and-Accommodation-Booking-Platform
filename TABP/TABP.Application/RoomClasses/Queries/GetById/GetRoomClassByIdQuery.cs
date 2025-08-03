using MediatR;
using TABP.Application.Common;
using TABP.Application.RoomClasses.Common;
namespace TABP.Application.RoomClasses.Queries.GetById
{
    public sealed record GetRoomClassByIdQuery(long Id) : IRequest<Result<RoomClassResponse>>;
}