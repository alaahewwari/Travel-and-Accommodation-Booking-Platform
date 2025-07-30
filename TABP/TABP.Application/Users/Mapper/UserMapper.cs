using Riok.Mapperly.Abstractions;
using TABP.Application.Users.Commands.Register;
using TABP.Domain.Entities;
namespace TABP.Application.Users.Mapper
{
    [Mapper]
    public static partial class UserMapper
    {
        public static User ToUserDomain(this RegisterUserCommand command)
        {
            var user = ToUserDomainInternal(command);
            user.CreatedAt = DateTime.UtcNow;
            return user;
        }
        [MapperIgnoreTarget(nameof(User.PasswordHash))]
        [MapperIgnoreTarget(nameof(User.Salt))]
        [MapperIgnoreTarget(nameof(User.Role))]
        private static partial User ToUserDomainInternal(RegisterUserCommand command);
    }
}