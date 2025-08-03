using MediatR;
using TABP.Application.Cities.Common;
using TABP.Application.Cities.Mapping;
using TABP.Application.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Cities.Queries.GetById
{
    public class GetCityByIdQueryHandler(ICityRepository cityRepository, CityMapper mapper)
        : IRequestHandler<GetCityByIdQuery, Result<CityResponse>>
    {
        public async Task<Result<CityResponse>> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            var city = await cityRepository.GetCityByIdAsync(request.Id, cancellationToken);
            if (city is null)
                return Result<CityResponse>.Failure(CityErrors.CityNotFound);
            var response = mapper.ToCityResponse(city);
            return Result<CityResponse>.Success(response);
        }
    }
}