using MediatR;
using TABP.Application.Cities.Common;
using TABP.Application.Common;
namespace TABP.Application.Cities.Queries.GetTrending
{
    public record GetTrendingCitiesQuery(int Count): IRequest<Result<IEnumerable<TrendingCityResponse>>>;
}