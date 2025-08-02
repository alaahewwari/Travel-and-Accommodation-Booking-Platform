using MediatR;
using TABP.Application.Common;
namespace TABP.Application.RoomClasses.Commands.AddAmenityToRoomClass
{
    public record AddAmenityToRoomClassCommand(long Id, long AmenityId) : IRequest<Result>;
}