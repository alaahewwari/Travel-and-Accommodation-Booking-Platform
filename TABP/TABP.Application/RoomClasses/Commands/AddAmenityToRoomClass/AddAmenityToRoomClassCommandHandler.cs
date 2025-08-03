using MediatR;
using TABP.Application.Amenities.Common;
using TABP.Application.Common;
using TABP.Application.RoomClassClasses.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.RoomClasses.Commands.AddAmenityToRoomClass
{
    public class AddAmenityToRoomClassCommandHandler(
        IAmenityRepository amenityRepository,
        IRoomClassRepository roomClassRepository) : IRequestHandler<AddAmenityToRoomClassCommand, Result>
    {
        public async Task<Result> Handle(AddAmenityToRoomClassCommand request, CancellationToken cancellationToken)
        {
            var existingAmenity =await amenityRepository.GetAmenityByIdAsync(request.AmenityId, cancellationToken);
            if (existingAmenity == null)
            {
                return Result.Failure(AmenityErrors.AmenityNotFound);
            }
            var existingRoomClass = await roomClassRepository.GetRoomClassByIdAsync(request.Id, cancellationToken);
            if (existingRoomClass == null)
            {
                return Result.Failure(RoomClassErrors.RoomClassNotFound);
            }
            await roomClassRepository.AssignAmenityToRoomClassAsync(existingAmenity, existingRoomClass, cancellationToken);
            return Result.Success();
        }
    }
}