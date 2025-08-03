using MediatR;
using TABP.Application.Cities.Common;
using TABP.Application.Common;
namespace TABP.Application.Cities.Queries.GetAll
{
    public record GetAllCitiesQuery() : IRequest<Result<IEnumerable<CityResponse>>>;
}