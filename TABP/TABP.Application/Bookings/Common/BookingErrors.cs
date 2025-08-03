using TABP.Application.Common;
namespace TABP.Application.Bookings.Common
{
    public static class BookingErrors
    {
        public static readonly Error BookingNotFound = new(
            Code: "Booking.NotFound",
            Description: "Booking with this ID does not exist."
        );
        public static readonly Error BookingAlreadyExists = new(
            Code: "Booking.AlreadyExists",
            Description: "Booking with this name already exists."
        );
        public static readonly Error InvalidBookingData = new(
            Code: "Booking.InvalidData",
            Description: "The provided Booking data is invalid."
        );
        public static readonly Error InvalidBookingDates = new(
            Code: "Booking.InvalidDates",
            Description: "Check-in date must be before check-out date."
        );
        public static readonly Error BookingOverlap = new(
            Code: "Booking.Overlap",
            Description: "The booking dates overlap with an existing booking for the same room."
        );
        public static readonly Error BookingAlreadyExist = new(
            Code: "Booking.AlreadyExist",
            Description: "A booking with the same details already exists."
        );
        public static readonly Error UnauthorizedAccess = new(
            Code: "Booking.UnauthorizedAccess",
            Description: "You do not have permission to access this booking."
        );
        public static readonly Error CancellationNotAllowed = new(
            Code: "Booking.CancellationNotAllowed",
            Description: "You cannot cancel a booking that has already started."
        );
        public static readonly Error BookingCreationFailed = new(
            Code: "Booking.CreationFailed",
            Description: "An error occurred while creating the booking."
        );
        public static readonly Error UnexpectedError = new(
            Code: "Booking.UnexpectedError",
            Description: "An unexpected error occurred while processing the booking."
        );
        public static readonly Error PaymentProcessingFailed = new(
            Code: "Booking.PaymentProcessingFailed",
            Description: "An error occurred while processing the payment for the booking."
        );
        public static readonly Error PaymentConfirmationFailed = new(
            Code: "Booking.PaymentConfirmationFailed",
            Description: "Payment confirmation failed. Please try again or contact support."
        );
        public static readonly Error BookingNotPending = new(
            Code: "Booking.NotPending",
            Description: "Booking is not in a pending state and cannot be confirmed."
        );
    }
}