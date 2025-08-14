using MediatR;
using TABP.Application.Common;
using TABP.Application.Users.Common;
namespace TABP.Application.Users.Commands.Register
{
    public record RegisterUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string RoleName
        ) : IRequest<Result<UserResponse>>;
}