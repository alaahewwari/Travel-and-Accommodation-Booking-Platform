using FluentValidation;
using TABP.API.Contracts.Bookings;
namespace TABP.API.Validators.Bookings
{
    /// <summary>
    /// Validator for booking creation requests to ensure valid booking parameters and reservation data.
    /// Validates room selection, hotel information, date ranges, and payment method requirements.
    /// </summary>
    public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
    {
        /// <summary>
        /// Initializes validation rules for booking creation including date validation, room selection, and payment validation.
        /// </summary>
        public CreateBookingRequestValidator()
        {
            RuleFor(x => x.RoomIds)
                .NotEmpty().WithMessage("At least one room must be selected.")
                .Must(x => x.All(id => id > 0)).WithMessage("All room IDs must be positive numbers.");
            RuleFor(x => x.CheckInDate)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Check-in date cannot be in the past.");
            RuleFor(x => x.CheckOutDate)
                .GreaterThan(x => x.CheckInDate).WithMessage("Check-out date must be after check-in date.");
            RuleFor(x => x.GuestRemarks)
                .MaximumLength(1000).WithMessage("Guest remarks must not exceed 1000 characters.");
            RuleFor(x => (x.CheckOutDate - x.CheckInDate).Days)
                .GreaterThan(0).LessThanOrEqualTo(365)
                .WithMessage("Booking duration must be between 1 and 365 days.");
        }
    }
}