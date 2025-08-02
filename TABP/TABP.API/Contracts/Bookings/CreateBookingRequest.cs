using TABP.Domain.Enums;
namespace TABP.API.Contracts.Bookings
{
    /// <summary>
    /// Request contract for creating a new hotel booking reservation.
    /// Contains all required information for room reservation including dates, payment, and guest details.
    /// </summary>
    /// <param name="RoomIds">List of room identifiers to be reserved for this booking.</param>
    /// <param name="HotelId">The identifier of the hotel where the reservation is being made.</param>
    /// <param name="CheckInDate">The date when guests will check into the hotel.</param>
    /// <param name="CheckOutDate">The date when guests will check out of the hotel.</param>
    /// <param name="PaymentMethod">The payment method to be used for this booking (credit card, cash, etc.).</param>
    /// <param name="GuestRemarks">Optional special requests or remarks from the guest.</param>
    public record CreateBookingRequest(
        IList<long> RoomIds,
        long HotelId,
        DateTime CheckInDate,
        DateTime CheckOutDate,
        PaymentMethod PaymentMethod,
        string? GuestRemarks
        );
}