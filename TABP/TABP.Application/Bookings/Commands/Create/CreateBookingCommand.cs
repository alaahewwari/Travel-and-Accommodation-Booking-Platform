using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Application.Common;
using TABP.Domain.Enums;
namespace TABP.Application.Bookings.Commands.Create
{
    public record CreateBookingCommand(
        IList<long> RoomIds,
        long HotelId,
        DateTime CheckInDate,
        DateTime CheckOutDate,
        string? GuestRemarks,
        PaymentMethod PaymentMethod
        ) :IRequest<Result<BookingCreationResponse>>;
}