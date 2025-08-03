using MediatR;
using TABP.Application.Common;
using TABP.Application.Roles.Common;
namespace TABP.Application.Roles.Commands.Update
{
    public record UpdateRoleCommand(int Id, string Name): IRequest<Result<RoleResponse>>;
}