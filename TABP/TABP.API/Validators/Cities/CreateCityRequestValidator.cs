using FluentValidation;
using TABP.API.Contracts.Cities;
namespace TABP.API.Validators.Cities
{
    /// <summary>
    /// Validator for city creation requests to ensure valid city information and postal code format.
    /// Validates city name, country, and post office code according to business rules.
    /// </summary>
    public class CreateCityRequestValidator : AbstractValidator<CreateCityRequest>
    {
        /// <summary>
        /// Initializes validation rules for city creation including name length limits and postal code format validation.
        /// </summary>
        public CreateCityRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("City name is required.")
                .MaximumLength(100).WithMessage("City name must not exceed 100 characters.");
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(100).WithMessage("Country must not exceed 100 characters.");
            RuleFor(x => x.PostOffice)
                .NotEmpty().WithMessage("Post office is required.")
                .Must(IsValidPostOffice).WithMessage("Post office must be exactly 5 characters long and numeric.");
        }

        /// <summary>
        /// Validates that the post office code is exactly 5 digits.
        /// </summary>
        /// <param name="input">The post office code to validate.</param>
        /// <returns>True if the post office code is valid, false otherwise.</returns>
        private bool IsValidPostOffice(string input)
        {
            return input.Length == 5 && input.All(char.IsDigit);
        }
    }
}