using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Application.Common;
namespace TABP.Application.Bookings.Commands.Update
{
    public record UpdateBookingCommand(
        long Id,
        DateTime CheckInDate,
        DateTime CheckOutDate,
        string? GuestRemarks
        ): IRequest<Result>;
}