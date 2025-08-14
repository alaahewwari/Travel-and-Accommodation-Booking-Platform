using FluentValidation;
using TABP.API.Contracts.Discounts;
namespace TABP.API.Validators.Discounts
{
    /// <summary>
    /// Validator for discount creation requests to ensure valid discount parameters and date ranges.
    /// Validates percentage values, date logic, and future date requirements.
    /// </summary>
    public class CreateDiscountRequestValidator : AbstractValidator<CreateDiscountRequest>
    {
        /// <summary>
        /// Initializes validation rules for discount creation including percentage limits and date range validation.
        /// </summary>
        public CreateDiscountRequestValidator()
        {
            RuleFor(x => x.Percentage)
                .InclusiveBetween(1, 100)
                .WithMessage("Discount percentage must be between 1 and 100.");
            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .WithMessage("Start date must be earlier than end date.");
            RuleFor(x => x.EndDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("End date must be in the future.");
        }
    }
}