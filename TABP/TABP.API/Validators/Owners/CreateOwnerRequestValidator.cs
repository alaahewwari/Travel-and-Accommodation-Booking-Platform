using FluentValidation;
using TABP.API.Contracts.Owners;
namespace TABP.API.Validators.Owners
{
    /// <summary>
    /// Validator for owner creation requests to ensure valid owner information and contact details.
    /// Validates personal information and phone number format for hotel owners.
    /// </summary>
    public class CreateOwnerRequestValidator : AbstractValidator<CreateOwnerRequest>
    {
        /// <summary>
        /// Initializes validation rules for owner creation including name validation and phone number format checking.
        /// </summary>
        public CreateOwnerRequestValidator()
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