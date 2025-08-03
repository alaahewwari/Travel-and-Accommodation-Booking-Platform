using MediatR;
using TABP.Application.Common;
using TABP.Application.RoomClasses.Common;
using TABP.Application.RoomClasses.Mapper;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.RoomClasses.Queries.GetAll
{
    public class GetAllRoomClassesQueryHandler(
        IRoomClassRepository repository)
        : IRequestHandler<GetAllRoomClassesQuery, Result<IEnumerable<RoomClassResponse>>>
    {
        public async Task<Result<IEnumerable<RoomClassResponse>>> Handle(
            GetAllRoomClassesQuery request,
            CancellationToken cancellationToken)
        {
            var roomClasses = await repository.GetAllRoomClassesAsync(cancellationToken);
            var result = roomClasses.Select(rc=>rc.ToRoomClassResponse());
            return Result<IEnumerable<RoomClassResponse>>.Success(result);
        }
    }
}