using MediatR;
using TABP.Application.Common;
using TABP.Application.RoomClassClasses.Common;
using TABP.Application.RoomClasses.Common;
using TABP.Application.RoomClasses.Mapper;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.RoomClasses.Commands.Update
{
    public sealed class UpdateRoomClassCommandHandler(
    IRoomClassRepository roomClassRepository)
    : IRequestHandler<UpdateRoomClassCommand, Result<RoomClassResponse>>
    {
        public async Task<Result<RoomClassResponse>> Handle(UpdateRoomClassCommand request, CancellationToken cancellationToken)
        {
            var existingRoomClass = await roomClassRepository.GetRoomClassByIdAsync(request.Id, cancellationToken);
            if (existingRoomClass is null)
            {
                return Result<RoomClassResponse>.Failure(RoomClassErrors.RoomClassNotFound);
            }
            var roomClassModel = request.ToRoomClassDomain();
            var updatedRoomClass = await roomClassRepository.UpdateRoomClassAsync(roomClassModel, cancellationToken);
            if (updatedRoomClass is null)
            {
                return Result<RoomClassResponse>.Failure(RoomClassErrors.NotModified);
            }
            var roomClass = updatedRoomClass.ToRoomClassResponse();
            return Result<RoomClassResponse>.Success(roomClass);
        }
    }
}