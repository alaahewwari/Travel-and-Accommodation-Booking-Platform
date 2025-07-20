using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Rules.Commands.Delete
{
    public record DeleteRoleCommand(int Id): IRequest<Result>;
}