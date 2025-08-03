using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Amenities.Commands.AssignToRoomClass
{
    public record AssignAmenityToRoomClassCommand(long Id, long RoomClassId) : IRequest<Result>;
}