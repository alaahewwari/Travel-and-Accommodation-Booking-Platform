using FluentValidation;
using TABP.API.Contracts.RoomClasses;
namespace TABP.API.Validators.RoomClasses
{
    /// <summary>
    /// Validator for room class update requests to ensure valid room type specification changes.
    /// Validates updated room descriptions, capacity modifications, and pricing adjustments.
    /// </summary>
    public class UpdateRoomClassRequestValidator : AbstractValidator<UpdateRoomClassRequest>
    {
        /// <summary>
        /// Initializes validation rules for room class updates including description limits, capacity validation, and pricing constraints.
        /// </summary>
        public UpdateRoomClassRequestValidator()
        {
            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
            RuleFor(x => x.PricePerNight)
                .GreaterThan(0).WithMessage("Price per night must be greater than 0.")
                .PrecisionScale(2, 10, true).WithMessage("Price must have up to 10 digits in total, with 2 after the decimal.");
            RuleFor(x => x.AdultsCapacity)
                .GreaterThan(0).WithMessage("Adults capacity must be at least 1.");
            RuleFor(x => x.ChildrenCapacity)
                .GreaterThanOrEqualTo(0).WithMessage("Children capacity cannot be negative.");
        }
    }
}