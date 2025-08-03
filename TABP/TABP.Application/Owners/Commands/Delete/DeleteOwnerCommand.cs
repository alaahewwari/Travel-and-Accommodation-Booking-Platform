using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Owners.Commands.Delete
{
    public record DeleteOwnerCommand(long Id) : IRequest<Result>;
}