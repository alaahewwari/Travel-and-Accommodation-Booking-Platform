using MediatR;
using TABP.Application.Common;
using TABP.Application.Roles.Commands.Create;
using TABP.Application.Roles.Common;
using TABP.Application.Roles.Mapper;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Rules.Commands.Create
{
    public class CreateRoleCommandHandler(IRoleRepository roleRepository) : IRequestHandler<CreateRoleCommand, Result<RoleResponse>>
    {
        public async Task<Result<RoleResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var existingRole = await roleRepository.GetRoleByNameAsync(request.Name, cancellationToken);
            if (existingRole is not null)
            {
                return Result<RoleResponse>.Failure(RoleErrors.RoleAlreadyExists);
            }
            var roleModel = request.ToRoleDomain();
            var role = await roleRepository.CreateRoleAsync(roleModel, cancellationToken);
            var response = role.ToRoleResponse();
            return Result<RoleResponse>.Success(response);
        }
    }
}