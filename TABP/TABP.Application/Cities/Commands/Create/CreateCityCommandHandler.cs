using MediatR;
using TABP.Application.Cities.Common;
using TABP.Application.Cities.Mapping;
using TABP.Application.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Cities.Commands.Create
{
    public class CreateCityCommandHandler(ICityRepository cityRepository, CityMapper mapper) : IRequestHandler<CreateCityCommand, Result<CityResponse>>
    {
        public async Task<Result<CityResponse>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var cityModel = mapper.ToCityDomain(request);
            var createdCity = await cityRepository.CreateCityAsync(cityModel, cancellationToken);
            var city = mapper.ToCityResponse(createdCity);
            return Result<CityResponse>.Success(city);
        }
    }
}