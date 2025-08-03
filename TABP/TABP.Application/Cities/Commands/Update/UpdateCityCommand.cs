using MediatR;
using Riok.Mapperly.Abstractions;
using TABP.Application.Cities.Common;
using TABP.Application.Common;
namespace TABP.Application.Cities.Commands.Update
{
    public record UpdateCityCommand(int Id, string Name, string Country, string PostOffice) : IRequest<Result<CityResponse>>;
}