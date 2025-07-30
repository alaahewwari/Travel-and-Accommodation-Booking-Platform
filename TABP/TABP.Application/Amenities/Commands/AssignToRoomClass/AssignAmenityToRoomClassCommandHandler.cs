using MediatR;
using TABP.Application.Amenities.Common;
using TABP.Application.Common;
using TABP.Application.RoomClassClasses.Common;
using TABP.Domain.Interfaces.Repositories;

namespace TABP.Application.Amenities.Commands.AssignToRoomClass
{
    public class AssignAmenityToRoomClassCommandHandler(
        IAmenityRepository amenityRepository,
        IRoomClassRepository roomClassRepository) : IRequestHandler<AssignAmenityToRoomClassCommand, Result>
    {
        public async Task<Result> Handle(AssignAmenityToRoomClassCommand request, CancellationToken cancellationToken)
        {
            var existingAmenity =await amenityRepository.GetAmenityByIdAsync(request.Id, cancellationToken);
            if (existingAmenity == null)
            {
                return Result<AmenityResponse>.Failure(AmenityErrors.AmenityNotFound);
            }
            var existingRoomClass = await roomClassRepository.GetRoomClassByIdAsync(request.RoomClassId, cancellationToken);
            if (existingRoomClass == null)
            {
                return Result<AmenityResponse>.Failure(RoomClassErrors.RoomClassNotFound);
            }
            await amenityRepository.AssignAmenityToRoomClassAsync(existingAmenity, existingRoomClass, cancellationToken);
            return Result.Success();
        }
    }
}