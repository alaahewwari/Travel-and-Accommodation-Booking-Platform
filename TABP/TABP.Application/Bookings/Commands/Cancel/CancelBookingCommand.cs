using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Bookings.Commands.Cancel
{
    public record CancelBookingCommand(long Id):IRequest<Result>;
}