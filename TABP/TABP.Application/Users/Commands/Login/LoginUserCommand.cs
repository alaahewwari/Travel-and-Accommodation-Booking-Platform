using MediatR;
using TABP.Application.Common;
using TABP.Application.Users.Common;

namespace TABP.Application.Users.Commands.Login
{
    public record LoginUserCommand(string Username, string Password) : IRequest<Result<LoginUserResponse>>;
}