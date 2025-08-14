using MediatR;
using TABP.Application.Common;
using TABP.Application.RoomClasses.Common;
namespace TABP.Application.RoomClasses.Commands.Create
{
    public sealed record CreateRoomClassCommand(
    byte Type,
    string? Description,
    string BriefDescription,
    decimal PricePerNight,
    int AdultsCapacity,
    int ChildrenCapacity,
    long HotelId
) : IRequest<Result<RoomClassResponse>>;
}