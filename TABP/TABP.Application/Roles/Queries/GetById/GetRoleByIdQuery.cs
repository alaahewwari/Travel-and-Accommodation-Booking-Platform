using MediatR;
using TABP.Application.Common;
using TABP.Application.Roles.Common;
namespace TABP.Application.Rules.Queries.GetById
{
    public record GetRoleByIdQuery(int Id) : IRequest<Result<RoleResponse>>;
}