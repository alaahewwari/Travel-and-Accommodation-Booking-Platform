using MediatR;
using TABP.Application.Common;
using TABP.Application.Rooms.Common;
using TABP.Application.Rooms.Mapper;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Rooms.Queries.GetById
{
    public class GetRoomByIdQueryHandler(
        IRoomRepository roomRepository
        ):IRequestHandler<GetRoomByIdQuery, Result<RoomResponse>>
    {
        public async Task<Result<RoomResponse>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var room = await roomRepository.GetRoomByIdAsync(request.Id, cancellationToken);
            if (room is null)
            {
                return Result<RoomResponse>.Failure(RoomErrors.RoomNotFound);
            }
            var response = room.ToRoomResponse();
            return Result<RoomResponse>.Success(response);
        }
    }
}