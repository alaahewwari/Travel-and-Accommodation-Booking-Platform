using FluentValidation;
using TABP.API.Contracts.Users;
namespace TABP.API.Validators.Users
{
    /// <summary>
    /// Validator for user registration requests to ensure valid account creation data.
    /// Validates user credentials, personal information, and role assignment requirements.
    /// </summary>
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        /// <summary>
        /// Initializes validation rules for user registration including email format, password strength, and role validation.
        /// </summary>
        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be in valid format.")
                .MaximumLength(255).WithMessage("Email must not exceed 255 characters.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]").WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(50).WithMessage("Role name must not exceed 50 characters.");
        }
    }
}