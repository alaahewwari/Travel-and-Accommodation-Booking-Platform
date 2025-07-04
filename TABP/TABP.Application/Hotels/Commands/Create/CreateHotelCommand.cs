using MediatR;
using TABP.Application.Common;
using TABP.Application.Hotels.Common;
namespace TABP.Application.Hotels.Commands.Create
{
    public record CreateHotelCommand(
    string Name,
    string? Description,
    string BriefDescription,
    string Address,
    byte StarRating,
    double LocationLatitude,
    double LocationLongitude,
    int CityId,
    long OwnerId
    ):IRequest<Result<long>>;
}