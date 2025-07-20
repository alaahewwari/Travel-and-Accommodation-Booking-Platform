using MediatR;
using TABP.Application.Common;
using TABP.Application.Roles.Common;
using TABP.Application.Roles.Mapper;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Rules.Queries.GetById
{
    public class GetRoleByIdQueryHandler(IRoleRepository roleRepository) : IRequestHandler<GetRoleByIdQuery, Result<RoleResponse>>
    {
        public async Task<Result<RoleResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await roleRepository.GetRoleByIdAsync(request.Id, cancellationToken);
            if (role is null)
            {
                return Result<RoleResponse>.Failure(RoleErrors.RoleNotFound);
            }
            var response = role.ToRoleResponse();
            return Result<RoleResponse>.Success(response);
        }
    }
}