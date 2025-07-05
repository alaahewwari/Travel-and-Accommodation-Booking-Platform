using MediatR;
using TABP.Application.Cities.Mapping;
using TABP.Application.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Cities.Commands.Create
{
    public class CreateCityCommandHandler(ICityRepository cityRepository, CityMapper mapper) : IRequestHandler<CreateCityCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var cityModel = mapper.ToCityDomain(request);
            var city = await cityRepository.CreateCityAsync(cityModel, cancellationToken);
            return Result<int>.Success(city.Id);
        }
    }
}