using MediatR;
using TABP.Application.Common;
using TABP.Application.RoomClasses.Common;
namespace TABP.Application.RoomClasses.Queries.GetAll
{
    public record GetAllRoomClassesQuery
         : IRequest<Result<IEnumerable<RoomClassResponse>>>;
}