using MediatR;
using TABP.Application.Cities.Common;
using TABP.Application.Cities.Mapper;
using TABP.Application.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Cities.Queries.GetAll
{
    public class GetAllCitiesQueryHandler(ICityRepository cityRepository)
        : IRequestHandler<GetAllCitiesQuery, Result<IEnumerable<CityForManagementResponse>>>
    {
        public async Task<Result<IEnumerable<CityForManagementResponse>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await cityRepository.GetAllCitiesAsync(cancellationToken);
            var result = cities.Select(c=>c.ToCityForManagementResponse()).ToList();
            return Result<IEnumerable<CityForManagementResponse>>.Success(result);
        }
    }
}