using MediatR;
using TABP.Application.Common;
using TABP.Application.RoomClasses.Common;
namespace TABP.Application.RoomClasses.Commands.Update
{
    public record UpdateRoomClassCommand(
     long Id,
     string Description,
     int AdultsCapacity,
     int ChildrenCapacity,
     decimal PricePerNight
    ) : IRequest<Result<RoomClassResponse>>;
}