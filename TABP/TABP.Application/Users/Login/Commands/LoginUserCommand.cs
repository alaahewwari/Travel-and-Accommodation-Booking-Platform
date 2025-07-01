using MediatR;
using TABP.Application.Common;
using TABP.Application.Users.Login.Common;
namespace TABP.Application.Users.Login.Commands
{
    public record LoginUserCommand(string Username, string Password) : IRequest<Result<LoginUserResponse>>;
}