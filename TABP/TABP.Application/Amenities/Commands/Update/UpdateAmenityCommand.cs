using MediatR;
using TABP.Application.Amenities.Common;
using TABP.Application.Common;
namespace TABP.Application.Amenities.Commands.Update
{
    public record UpdateAmenityCommand(long Id, string Name, string Description): IRequest<Result<AmenityResponse>>;
}