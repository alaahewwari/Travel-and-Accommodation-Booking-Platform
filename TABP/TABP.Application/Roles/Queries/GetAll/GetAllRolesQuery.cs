using MediatR;
using TABP.Application.Common;
using TABP.Application.Roles.Common;
namespace TABP.Application.Rules.Queries.GetAll
{
    public record GetAllRolesQuery: IRequest<Result<IEnumerable<RoleResponse>>>;
}