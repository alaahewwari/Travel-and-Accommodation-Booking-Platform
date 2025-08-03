using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.Users;
using TABP.Application.Users.Commands.Login;
namespace TABP.API.Mapping
{
    [Mapper]
    public partial class IdentityMapper
    {
        [MapProperty(nameof(LoginUserRequest.Username), nameof(LoginUserCommand.Username))]
        [MapProperty(nameof(LoginUserRequest.Password), nameof(LoginUserCommand.Password))]
        public partial LoginUserCommand ToCommand(LoginUserRequest request);
    }
}