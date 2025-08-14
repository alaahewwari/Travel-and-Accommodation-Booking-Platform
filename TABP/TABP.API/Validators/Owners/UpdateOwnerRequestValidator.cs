using FluentValidation;
using TABP.API.Contracts.Owners;
namespace TABP.API.Validators.Owners
{
    /// <summary>
    /// Validator for owner update requests to ensure valid owner information modifications.
    /// Validates updated personal information and contact details for hotel owners.
    /// </summary>
    public class UpdateOwnerRequestValidator : AbstractValidator<UpdateOwnerRequest>
    {
        /// <summary>
        /// Initializes validation rules for owner updates including name validation and phone number format checking.
        /// </summary>
        public UpdateOwnerRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be in valid international format.");
        }
    }
}