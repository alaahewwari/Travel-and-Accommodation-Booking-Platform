using FluentValidation;
using TABP.API.Contracts.Reviews;
namespace TABP.API.Validators.Reviews
{
    /// <summary>
    /// Validator for review creation requests to ensure valid hotel reference, rating, and comment content.
    /// Validates rating range (1–5) and enforces maximum comment length.
    /// </summary>
    public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
    {
        /// <summary>
        /// Initializes validation rules for creating a review, including hotel ID validation, 
        /// rating bounds, and comment length restrictions.
        /// </summary>
        public CreateReviewRequestValidator()
        {
            RuleFor(x => x.HotelId)
                .GreaterThan(0).WithMessage("HotelId must be greater than zero.");
            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
            RuleFor(x => x.Comment)
                .MaximumLength(1000)
                .WithMessage("Comment must not exceed 1000 characters.");
        }
    }
}