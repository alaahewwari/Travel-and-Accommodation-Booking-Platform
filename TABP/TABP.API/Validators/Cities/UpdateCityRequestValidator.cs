using FluentValidation;
using TABP.API.Contracts.Cities;
namespace TABP.API.Validators.Cities
{
    public class UpdateCityRequestValidator : AbstractValidator<UpdateCityRequest>
    {
        public UpdateCityRequestValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("City name must not exceed 100 characters.");
            RuleFor(x => x.Country)
                .MaximumLength(100).WithMessage("Country must not exceed 100 characters.");
            RuleFor(x => x.PostOffice)
                .Must(IsValidPostOffice).WithMessage("Post office must be exactly 5 characters long and numeric.");
        }
        private bool IsValidPostOffice(string input)
        {
            return input.Length == 5 && input.All(char.IsDigit);
        }
    }
}