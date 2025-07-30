using MediatR;
using TABP.Application.Common;
using TABP.Application.Roles.Common;
using TABP.Application.Users.Common.Errors;
using TABP.Application.Users.Mapper;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
namespace TABP.Application.Users.Commands.Register
{
    public class RegisterUserCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHasher passwordHasher) : IRequestHandler<RegisterUserCommand, Result>
    {
        public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var role = await roleRepository.GetRoleByNameAsync(request.RoleName,cancellationToken);
            if(role is null)
            {
                return Result.Failure(RoleErrors.RoleNotFound);
            }
            var existingUser = await userRepository.GetUserByEmailAsync(request.Email,cancellationToken);
            if (existingUser is not null)
            {
                return Result.Failure(UserErrors.UserAlreadyExist);
            }
            var user = request.ToUserDomain();
            user.RoleId = role.Id;
            user.Username = GenerateUsername();
            (user.PasswordHash, user.Salt) = passwordHasher.HashPassword(request.Password);
            var createdUser= await userRepository.CreateAsync(user, cancellationToken);
            return Result.Success();
        }
        private static string GenerateUsername()
        {
            return $"user_{Guid.NewGuid().ToString("N")[..8]}";
        }
    }
}