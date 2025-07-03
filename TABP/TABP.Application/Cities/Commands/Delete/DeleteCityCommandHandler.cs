using MediatR;
using TABP.Application.Cities.Common;
using TABP.Application.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Cities.Commands.Delete
{
    public class DeleteCityCommandHandler(ICityRepository cityRepository)
         : IRequestHandler<DeleteCityCommand, Result>
    {
        public async Task<Result> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var city = await cityRepository.GetCityByIdAsync(request.Id, cancellationToken);
            if (city is null)
                return Result.Failure(CityErrors.CityNotFound);
            await cityRepository.DeleteCityAsync(city.Id, cancellationToken);
            return Result.Success();
        }
    }
}