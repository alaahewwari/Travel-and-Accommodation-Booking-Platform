using MediatR;
using TABP.Application.Common;
using TABP.Application.Rooms.Common;
using TABP.Application.Rooms.Mapper;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Rooms.Queries.GetAll
{
    public class GetAllRoomsQueryHandler(
        IRoomRepository roomRepository
        ) : IRequestHandler<GetAllRoomsQuery, Result<IEnumerable<RoomForManagementResponse>>>
    {
        public async Task<Result<IEnumerable<RoomForManagementResponse>>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            var rooms = await roomRepository.GetAllRoomsAsync(cancellationToken);
            var response = rooms.Select(r=>r.ToRoomForManagementResponse());
            return Result<IEnumerable<RoomForManagementResponse>>.Success(response);
        }
    }
}