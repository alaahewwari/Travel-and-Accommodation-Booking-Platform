using FluentValidation;
using TABP.API.Contracts.Rules;
namespace TABP.API.Validators.Roles
{
    /// <summary>
    /// Validator for role creation requests to ensure valid role information and naming conventions.
    /// Validates role names and ensures compliance with security and naming standards.
    /// </summary>
    public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
    {
        /// <summary>
        /// Initializes validation rules for role creation including name format and length validation.
        /// </summary>
        public CreateRoleRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(50).WithMessage("Role name must not exceed 50 characters.")
                .Matches(@"^[A-Za-z\s]+$").WithMessage("Role name can only contain letters and spaces.");
        }
    }
}