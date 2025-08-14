using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.Rules;
using TABP.Application.Roles.Commands.Create;
using TABP.Application.Roles.Commands.Update;
namespace TABP.API.Mappers
{
    [Mapper]
    public static partial class RoleMapper
    {
        public static partial CreateRoleCommand ToCommand(this CreateRoleRequest request);
        public static partial UpdateRoleCommand ToCommand(this UpdateRoleRequest request, int Id);
    }
}