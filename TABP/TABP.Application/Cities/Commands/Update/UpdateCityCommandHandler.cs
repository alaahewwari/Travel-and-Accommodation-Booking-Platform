using MediatR;
using TABP.Application.Cities.Common;
using TABP.Application.Cities.Mapping;
using TABP.Application.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Cities.Commands.Update
{
    public class UpdateCityCommandHandler(ICityRepository cityRepository, CityMapper mapper)
        : IRequestHandler<UpdateCityCommand, Result<CityResponse>>
    {
        public async Task<Result<CityResponse>> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var existingCity = await cityRepository.GetCityByIdAsync(request.Id, cancellationToken);
            if (existingCity is null)
            {
                return Result<CityResponse>.Failure(CityErrors.CityNotFound);
            }
            var cityModel = mapper.ToCityDomain(request);
            var updatedCity = await cityRepository.UpdateCityAsync(cityModel, cancellationToken);
            var city = mapper.ToCityResponse(updatedCity!);
            return Result<CityResponse>.Success(city);
        }
    }
}