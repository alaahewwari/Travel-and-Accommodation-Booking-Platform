using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Cities.Commands.Create
{
    public record CreateCityCommand(string Name, string Country, string PostOffice) : IRequest<Result<int>>;
}