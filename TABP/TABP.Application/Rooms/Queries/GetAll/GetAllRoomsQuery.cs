using MediatR;
using TABP.Application.Common;
using TABP.Application.Rooms.Common;
namespace TABP.Application.Rooms.Queries.GetAll
{
    public record GetAllRoomsQuery : IRequest<Result<IEnumerable<RoomForManagementResponse>>>;
}