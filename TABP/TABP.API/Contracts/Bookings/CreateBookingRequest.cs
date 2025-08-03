using TABP.Domain.Enums;

namespace TABP.API.Contracts.Bookings
{
    public record CreateBookingRequest(
        IList<long> RoomIds,
        long HotelId,
        DateTime CheckInDate,
        DateTime CheckoutDate,
        PaymentMethod PaymentMethod,
        string? GuestRemarks
        );
}