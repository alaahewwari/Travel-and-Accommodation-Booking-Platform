using FluentValidation;
using TABP.API.Contracts.Users;
namespace TABP.API.Validators.Users
{
    /// <summary>
    /// Validator for user login requests to ensure valid authentication credentials.
    /// Validates username and password format for user authentication.
    /// </summary>
    public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
    {
        /// <summary>
        /// Initializes validation rules for user login including username and password requirements.
        /// </summary>
        public LoginUserRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(255).WithMessage("Username must not exceed 255 characters.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}