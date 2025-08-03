using Riok.Mapperly.Abstractions;
using TABP.Application.Roles.Commands.Create;
using TABP.Application.Roles.Commands.Update;
using TABP.Application.Roles.Common;
using TABP.Domain.Entities;
namespace TABP.Application.Roles.Mapper
{
    [Mapper]
    public static partial class RoleMapper
    {
        public static partial RoleResponse ToRoleResponse(this Role Role);
        public static partial Role ToRoleDomain(this CreateRoleCommand command);
        public static partial Role ToRoleDomain(this UpdateRoleCommand command);
    }
}