using MediatR;
using TABP.Application.Cities.Common;
using TABP.Application.Cities.Mapping;
using TABP.Application.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Cities.Queries.GetAll
{
    public class GetAllCitiesQueryHandler(ICityRepository cityRepository, CityMapper mapper)
        : IRequestHandler<GetAllCitiesQuery, Result<IEnumerable<CityResponse>>>
    {
        public async Task<Result<IEnumerable<CityResponse>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await cityRepository.GetAllCitiesAsync(cancellationToken);
            var result = cities.Select(mapper.ToCityResponse).ToList();
            return Result<IEnumerable<CityResponse>>.Success(result);
        }
    }
}