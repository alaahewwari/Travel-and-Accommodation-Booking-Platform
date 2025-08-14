using FluentValidation;
using TABP.API.Contracts.Cities;
namespace TABP.API.Validators.Cities
{
    /// <summary>
    /// Validator for city update requests to ensure valid city information updates.
    /// Validates optional fields including name, country, and postal code format.
    /// </summary>
    public class UpdateCityRequestValidator : AbstractValidator<UpdateCityRequest>
    {
        /// <summary>
        /// Initializes validation rules for city updates with optional field validation and postal code format checking.
        /// </summary>
        public UpdateCityRequestValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("City name must not exceed 100 characters.");
            RuleFor(x => x.Country)
                .MaximumLength(100).WithMessage("Country must not exceed 100 characters.");
            RuleFor(x => x.PostOffice)
                .Must(IsValidPostOffice).WithMessage("Post office must be exactly 5 characters long and numeric.");
        }
        /// <summary>
        /// Validates that the post office code is exactly 5 digits when provided.
        /// </summary>
        /// <param name="input">The post office code to validate.</param>
        /// <returns>True if the post office code is valid or null, false otherwise.</returns>
        private bool IsValidPostOffice(string input)
        {
            return input == null || (input.Length == 5 && input.All(char.IsDigit));
        }
    }
}