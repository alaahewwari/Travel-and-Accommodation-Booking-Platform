using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;

namespace TABP.Application.Hotels.Commands.Update
{
    public record UpdateHotelCommand(
        long Id,
        string Name,
        int CityId,
        long OwnerId,
        double LocationLatitude,
        double LocationLongitude,
        string Address
    ) : IRequest<Result<HotelResponse>>;
}