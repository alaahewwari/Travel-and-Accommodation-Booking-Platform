using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
using TABP.Application.Rooms.Common;
using TABP.Application.Rooms.Mapper;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Rooms.Commands.Create
{
    public class CreateRoomCommandHandler(
        IRoomRepository roomRepository,
        IHotelRepository hotelRepository
        ) : IRequestHandler<CreateRoomCommand, Result<RoomResponse>>
    {
        public async Task<Result<RoomResponse>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId, cancellationToken);
            if(hotel is null)
            {
                return Result<RoomResponse>.Failure(HotelErrors.HotelNotFound);
            }
            var existingRoom = await roomRepository.GetRoomByHotelAsync(hotel, cancellationToken);
            if (existingRoom != null)
            {
                return Result<RoomResponse>.Failure(RoomErrors.RoomAlreadyExists);
            }
            var room = request.ToRoomDomain();
            var createdRoom = await roomRepository.CreateRoomAsync(room, cancellationToken);
            var response = createdRoom.ToRoomResponse();
            return Result<RoomResponse>.Success(response);
        }
    }
}