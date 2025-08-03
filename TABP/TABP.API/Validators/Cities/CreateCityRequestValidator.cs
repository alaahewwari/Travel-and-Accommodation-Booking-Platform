using FluentValidation;
using TABP.API.Contracts.City;
namespace TABP.API.Validators.Cities
{
    public class CreateCityRequestValidator : AbstractValidator<CreateCityRequest>
    {
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
        private bool IsValidPostOffice(string input)
        {
            return input.Length == 5 && input.All(char.IsDigit);
        }
    }
}