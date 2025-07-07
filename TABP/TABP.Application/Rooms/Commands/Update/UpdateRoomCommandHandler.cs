using MediatR;
using TABP.Application.Common;
using TABP.Application.Rooms.Common;
using TABP.Application.Rooms.Mapper;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Rooms.Commands.Update
{
    public class UpdateRoomCommandHandler(
        IRoomRepository roomRepository
        ) : IRequestHandler<UpdateRoomCommand, Result<RoomResponse>>
    {
        public async Task<Result<RoomResponse>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var existingRoom =await roomRepository.GetRoomByIdAsync(request.Id, cancellationToken);
            if (existingRoom is null)
            {
                return Result<RoomResponse>.Failure(RoomErrors.RoomNotFound);
            }
            var roomCommand = request.ToRoomDomain();
            var updatedRoom = await roomRepository.UpdateRoomAsync(roomCommand, cancellationToken);
            if (updatedRoom is null)
            {
                return Result<RoomResponse>.Failure(RoomErrors.NotModified);
            }
            var roomResponse = updatedRoom.ToRoomResponse();
            return Result<RoomResponse>.Success(roomResponse);
        }
    }
}
