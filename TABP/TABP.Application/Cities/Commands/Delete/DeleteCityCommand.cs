using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Cities.Commands.Delete
{
    public record DeleteCityCommand(int Id) : IRequest<Result>;
}