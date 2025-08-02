using FluentValidation;
using TABP.API.Contracts.Rules;
namespace TABP.API.Validators.Roles
{
    /// <summary>
    /// Validator for role update requests to ensure valid role information modifications.
    /// Validates updated role names and ensures compliance with security and naming standards.
    /// </summary>
    public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
    {
        /// <summary>
        /// Initializes validation rules for role updates including name format and length validation.
        /// </summary>
        public UpdateRoleRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(50).WithMessage("Role name must not exceed 50 characters.")
                .Matches(@"^[A-Za-z\s]+$").WithMessage("Role name can only contain letters and spaces.");
        }
    }
}