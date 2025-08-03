using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.Users;
using TABP.Application.Users.Commands.Login;
using TABP.Application.Users.Commands.Register;
namespace TABP.API.Mapping
{
    [Mapper]
    public static partial class IdentityMapping
    {
        public static partial LoginUserCommand ToCommand(this LoginUserRequest request);
        public static partial RegisterUserCommand ToCommand(this RegisterUserRequest request);
    }
}