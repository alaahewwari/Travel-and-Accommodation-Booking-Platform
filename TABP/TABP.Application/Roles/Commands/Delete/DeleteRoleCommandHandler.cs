using MediatR;
using TABP.Application.Common;
using TABP.Application.Roles.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Rules.Commands.Delete
{
    public class DeleteRoleCommandHandler(IRoleRepository roleRepository) : IRequestHandler<DeleteRoleCommand, Result>
    {
        public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var existingRole = await roleRepository.GetRoleByIdAsync(request.Id, cancellationToken);
            if (existingRole == null)
            {
                return Result.Failure(RoleErrors.RoleNotFound);
            }
            var isDeleted = await roleRepository.DeleteRoleAsync(existingRole.Id, cancellationToken);
            if (!isDeleted)
            {
                return Result.Failure(RoleErrors.RoleDeletionFailed);
            }
            return Result.Success();
        }
    }
}