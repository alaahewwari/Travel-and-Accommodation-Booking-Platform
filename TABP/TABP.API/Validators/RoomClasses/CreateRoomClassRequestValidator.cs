using FluentValidation;
using TABP.API.Contracts.RoomClasses;
namespace TABP.API.Validators.RoomClasses
{
    /// <summary>
    /// Validator for room class creation requests to ensure valid room type specifications and pricing.
    /// Validates room descriptions, capacity limits, pricing constraints, and hotel references.
    /// </summary>
    public class CreateRoomClassRequestValidator : AbstractValidator<CreateRoomClassRequest>
    {
        /// <summary>
        /// Initializes validation rules for room class creation including description limits, capacity validation, and pricing constraints.
        /// </summary>
        public CreateRoomClassRequestValidator()
        {
            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
            RuleFor(x => x.BriefDescription)
                .NotEmpty().WithMessage("Brief description is required.")
                .MaximumLength(255).WithMessage("Brief description must not exceed 255 characters.");
            RuleFor(x => x.PricePerNight)
                .GreaterThan(0).WithMessage("Price per night must be greater than 0.")
                .PrecisionScale(2, 10, true).WithMessage("Price must have up to 10 digits in total, with 2 after the decimal.");
            RuleFor(x => x.AdultsCapacity)
                .GreaterThan(0).WithMessage("Adults capacity must be at least 1.");
            RuleFor(x => x.ChildrenCapacity)
                .GreaterThanOrEqualTo(0).WithMessage("Children capacity cannot be negative.");
            RuleFor(x => x.HotelId)
                .GreaterThan(0).WithMessage("Hotel ID must be a positive number.");
        }
    }
}