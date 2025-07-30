using MediatR;
using TABP.Application.Amenities.Common;
using TABP.Application.Common;
namespace TABP.Application.Amenities.Queries.GetById
{
    public record GetAmenityByIdQuery(long Id) : IRequest<Result<AmenityResponse>>;
}