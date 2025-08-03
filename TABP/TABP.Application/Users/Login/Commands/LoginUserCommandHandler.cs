using MediatR;
using TABP.Application.Common;
using TABP.Application.Users.Login.Common;
using TABP.Application.Users.Login.Errors;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
namespace TABP.Application.Users.Login.Commands
{
    public class LoginUserCommandHandler(IUserRepository userRepository, IJwtGenerator jwtGenerator) : IRequestHandler<LoginUserCommand, Result<LoginUserResponse>>
    {
        public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.AuthenticateUserAsync(request.Username, request.Password, cancellationToken);
            if (user is null)
            {
                return Result<LoginUserResponse>.Failure(LoginUserErrors.InvalidCredentials);
            }
            var token = jwtGenerator.GenerateToken(user);
            var response = new LoginUserResponse(token);
            return Result<LoginUserResponse>.Success(response);
        }
    }
}