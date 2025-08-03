using MediatR;
using TABP.Application.Amenities.Common;
using TABP.Application.Common;
namespace TABP.Application.Amenities.Commands.Create
{
    public record CreateAmenityCommand(string Name, string Description)
     : IRequest<Result<AmenityResponse>>;
}