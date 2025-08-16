using FluentValidation;
using TABP.API.Contracts.Reviews;
namespace TABP.API.Validators.Reviews
{
    /// <summary>
    /// Validator for review update requests to ensure valid hotel reference, rating, and comment content.
    /// Ensures updated reviews maintain correct rating range and valid comment length.
    /// </summary>
    public class UpdateReviewRequestValidator : AbstractValidator<UpdateReviewRequest>
    {
        /// <summary>
        /// Initializes validation rules for updating a review, including hotel ID validation, 
        /// rating bounds, and required comment with length restrictions.
        /// </summary>
        public UpdateReviewRequestValidator()
        {
            RuleFor(r => r.HotelId)
                .GreaterThan(0).WithMessage("HotelId must be a positive number.");
            RuleFor(r => r.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
            RuleFor(r => r.Comment)
                .NotEmpty().WithMessage("Comment is required.")
                .MaximumLength(1000).WithMessage("Comment cannot exceed 1000 characters.");
        }
    }
}