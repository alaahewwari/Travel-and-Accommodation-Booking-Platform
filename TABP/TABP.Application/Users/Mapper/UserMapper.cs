using Riok.Mapperly.Abstractions;
using TABP.Application.Users.Commands.Register;
using TABP.Domain.Entities;
namespace TABP.Application.Users.Mapper
{
    [Mapper]
    public static partial class UserMapper
    {
        public static partial User ToUserDomain(this RegisterUserCommand command);
    }
}