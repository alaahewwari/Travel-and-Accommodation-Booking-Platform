using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.RoomClasses.Common;
using TABP.Application.RoomClasses.Mapper;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.RoomClasses.Commands.Create
{
    public class CreateRoomClassCommandHandler(
    IRoomClassRepository roomClassRepository,
    IHotelRepository hotelRepository)
    : IRequestHandler<CreateRoomClassCommand, Result<RoomClassResponse>>
    {
        public async Task<Result<RoomClassResponse>> Handle(CreateRoomClassCommand request, CancellationToken cancellationToken)
        {
            var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId, cancellationToken);
            if(hotel is null)
            {
                return Result<RoomClassResponse>.Failure(HotelErrors.HotelNotFound);
            }
            var roomClass = request.ToRoomClassDomain(hotel);
            var createdRoomClass = await roomClassRepository.CreateRoomClassAsync(roomClass, cancellationToken);
            var response = createdRoomClass.ToRoomClassResponse();
            return Result<RoomClassResponse>.Success(response);
        }
    }
}