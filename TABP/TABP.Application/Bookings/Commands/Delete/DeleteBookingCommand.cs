using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Bookings.Commands.Delete
{
    public record DeleteBookingCommand(long Id):IRequest<Result>;
}