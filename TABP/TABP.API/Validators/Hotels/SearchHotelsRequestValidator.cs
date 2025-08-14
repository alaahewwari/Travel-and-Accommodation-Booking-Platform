using FluentValidation;
using TABP.API.Contracts.Hotels;
namespace TABP.API.Validators.Hotels
{
    /// <summary>
    /// Validator for hotel search requests to ensure valid search parameters and date ranges.
    /// Validates search criteria, guest counts, date logic, and pagination parameters.
    /// </summary>
    public class SearchHotelsRequestValidator : AbstractValidator<SearchHotelsRequest>
    {
        /// <summary>
        /// Initializes validation rules for hotel search including date validation, guest limits, and pagination constraints.
        /// </summary>
        public SearchHotelsRequestValidator()
        {
            RuleFor(x => x.CheckInDate)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Check-in date cannot be in the past.");
            RuleFor(x => x.CheckOutDate)
                .GreaterThan(x => x.CheckInDate).WithMessage("Check-out date must be after check-in date.");
            RuleFor(x => x.Adults)
                .GreaterThan(0).WithMessage("At least one adult is required.")
                .LessThanOrEqualTo(10).WithMessage("Maximum 10 adults allowed per search.");
            RuleFor(x => x.Children)
                .GreaterThanOrEqualTo(0).WithMessage("Children count cannot be negative.")
                .LessThanOrEqualTo(10).WithMessage("Maximum 10 children allowed per search.");
            RuleFor(x => x.Rooms)
                .GreaterThan(0).WithMessage("At least one room is required.")
                .LessThanOrEqualTo(5).WithMessage("Maximum 5 rooms allowed per search.");
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Page number must be greater than 0.")
                .When(x => x.Page.HasValue);
            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.")
                .When(x => x.PageSize.HasValue);
        }
    }
}