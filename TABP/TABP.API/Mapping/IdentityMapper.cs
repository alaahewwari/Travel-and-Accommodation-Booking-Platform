using Riok.Mapperly.Abstractions;
using TABP.API.Contracts.User;
using TABP.Application.Users.Login.Commands;
using TABP.Application.Users.Login.Common;

namespace TABP.API.Mapping
{
    [Mapper]
    public partial class IdentityMapper
    {
        public partial LoginUserCommand ToCommand(LoginUserRequest request);
        public partial LoginUserResponse ToResponse(LoginUserResponse source);
    }
}
