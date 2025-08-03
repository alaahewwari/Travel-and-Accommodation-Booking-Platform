using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Application.Common;
namespace TABP.Application.Bookings.Commands.Create
{
    public record CreateBookingCommand(
        IList<long> RoomIds,
        long HotelId,
        DateTime CheckInDate,
        DateTime CheckOutDate,
        string? GuestRemarks,
        string PaymentMethodId
        ) :IRequest<Result<BookingCreationResponse>>;
}