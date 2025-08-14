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
    /// <param name="PaymentMethodId">Identifier for the payment method used for this booking, such as a credit card or PayPal.</param>
    /// <param name="GuestRemarks">Optional special requests or remarks from the guest.</param>
    public record CreateBookingRequest(
        IList<long> RoomIds,
        long HotelId,
        DateTime CheckInDate,
        DateTime CheckOutDate,
        string PaymentMethodId,
        string? GuestRemarks
        );
}