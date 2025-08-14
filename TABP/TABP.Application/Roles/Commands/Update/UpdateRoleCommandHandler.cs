using MediatR;
using TABP.Application.Common;
using TABP.Application.Roles.Common;
using TABP.Application.Roles.Mapper;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Roles.Commands.Update
{
    public class UpdateRoleCommandHandler(IRoleRepository roleRepository) : IRequestHandler<UpdateRoleCommand, Result<RoleResponse>>
    {
        public async Task<Result<RoleResponse>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var existingRole =await roleRepository.GetRoleByNameAsync(request.Name, cancellationToken);
            if (existingRole is null)
            {
                return Result<RoleResponse>.Failure(RoleErrors.RoleNotFound);
            }
            var role = request.ToRoleDomain();
            var updatedRole = await roleRepository.UpdateRoleAsync(role, cancellationToken);
            if(updatedRole is null)
            {
                return Result<RoleResponse>.Failure(RoleErrors.RoleUpdateFailed);
            }
            var response = updatedRole.ToRoleResponse();
            return Result<RoleResponse>.Success(response);
        }
    }
}