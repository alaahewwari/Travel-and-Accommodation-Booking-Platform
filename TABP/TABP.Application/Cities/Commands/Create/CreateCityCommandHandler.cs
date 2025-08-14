using MediatR;
using TABP.Application.Cities.Common;
using TABP.Application.Cities.Mapper;
using TABP.Application.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Cities.Commands.Create
{
    public class CreateCityCommandHandler(ICityRepository cityRepository) : IRequestHandler<CreateCityCommand, Result<CityResponse>>
    {
        public async Task<Result<CityResponse>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var cityModel = request.ToCityDomain();
            var city = await cityRepository.CreateCityAsync(cityModel, cancellationToken);
            var response = city.ToCityResponse();
            return Result<CityResponse>.Success(response);
        }
    }
}