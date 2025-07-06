using MediatR;
using TABP.Application.Cities.Common;
using TABP.Application.Cities.Mapper;
using TABP.Application.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Cities.Queries.GetTrending
{
    public class GetTrendingCitiesQueryHandler(
        ICityRepository cityRepository
        ) : IRequestHandler<GetTrendingCitiesQuery, Result<IEnumerable<TrendingCityResponse>>>
    {
        public async Task<Result<IEnumerable<TrendingCityResponse>>> Handle(GetTrendingCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await cityRepository.GetMostBookedCitiesAsync(request.Count, cancellationToken);
            var cityResponses = cities.Select( c=> c.ToTrendingCityResponse());
            return Result<IEnumerable<TrendingCityResponse>>.Success(cityResponses);
        }
    }
}