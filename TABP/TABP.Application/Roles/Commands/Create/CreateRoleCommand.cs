using MediatR;
using TABP.Application.Common;
using TABP.Application.Roles.Common;
namespace TABP.Application.Roles.Commands.Create
{
    public record CreateRoleCommand(string Name): IRequest<Result<RoleResponse>>;
}