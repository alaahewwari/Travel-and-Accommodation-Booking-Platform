using MediatR;
using TABP.Application.Common;
using TABP.Application.Roles.Common;
namespace TABP.Application.Rules.Queries.GetAll
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<IEnumerable<RoleResponse>>>
    {
        Task<Result<IEnumerable<RoleResponse>>> IRequestHandler<GetAllRolesQuery, Result<IEnumerable<RoleResponse>>>.Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}