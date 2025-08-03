using FluentValidation;
using TABP.Application.Bookings.Commands.Create;

namespace TABP.API.Validators.Booking
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.HotelId)
                .NotEmpty()
                .WithMessage("Hotel ID is required");
            RuleFor(x => x.RoomIds)
                .NotEmpty()
                .WithMessage("At least one room must be selected");
            RuleFor(x => x.CheckInDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Check-in date cannot be in the past");
            RuleFor(x => x.CheckOutDate)
                .GreaterThan(x => x.CheckInDate)
                .WithMessage("Check-out date must be after check-in date");
            RuleFor(x => (x.CheckOutDate - x.CheckInDate).Days)
                .GreaterThan(0)
                .LessThanOrEqualTo(365)
                .WithMessage("Booking duration must be between 1 and 365 days");
        }
    }
}