using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Application.Common;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
namespace TABP.Application.Bookings.Commands.Cancel
{
    public class CancelBookingCommandHandler(
        IBookingRepository bookingRepository,
        IUserContext userContext) : IRequestHandler<CancelBookingCommand, Result>
    {
        public async Task<Result> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await bookingRepository.GetByIdAsync(request.Id, cancellationToken);
            if (booking is null)
            {
                return Result.Failure(BookingErrors.BookingNotFound);
            }
            if (booking.UserId != userContext.UserId)
            {
                return Result.Failure(BookingErrors.UnauthorizedAccess);
            }
            if (booking!.CheckInDate <= DateTime.UtcNow)
            {
                return Result.Failure(BookingErrors.CancellationNotAllowed);
            }
            await bookingRepository.CancelAsync(booking, cancellationToken);
            return Result.Success();
        }
    }
}