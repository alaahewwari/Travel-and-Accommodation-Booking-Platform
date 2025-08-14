using MediatR;
using TABP.Application.Common;
using TABP.Application.Rooms.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Rooms.Commands.Delete
{
    public class DeleteRoomCommandHandler(IRoomRepository roomRepository) : IRequestHandler<DeleteRoomCommand, Result>
    {
        public async Task<Result> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var existingRoom = await roomRepository.GetRoomByIdAsync(request.Id, cancellationToken);
            if (existingRoom is null)
            {
                return Result.Failure(RoomErrors.RoomNotFound);
            }
            await roomRepository.DeleteRoomAsync(request.Id, cancellationToken);
            return Result.Success();
        }
    }
}