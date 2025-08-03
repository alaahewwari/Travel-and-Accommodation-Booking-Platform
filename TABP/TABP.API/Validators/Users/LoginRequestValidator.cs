using FluentValidation;
using TABP.API.Contracts.Users;
namespace TABP.API.Validators.Users
{
    public class LoginRequestValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username)
              .NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.Password)
              .NotEmpty().WithMessage("Password is required.");
        }
    }
}
