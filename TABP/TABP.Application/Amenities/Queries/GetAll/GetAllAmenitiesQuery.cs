using MediatR;
using TABP.Application.Amenities.Common;
using TABP.Application.Common;
namespace TABP.Application.Amenities.Queries.GetAll
{
    public record GetAllAmenitiesQuery : IRequest<Result<IEnumerable<AmenityResponse>>>;
}