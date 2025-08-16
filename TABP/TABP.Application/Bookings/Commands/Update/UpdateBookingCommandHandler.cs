using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Application.Bookings.Mapper;
using TABP.Application.Common;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Bookings.Commands.Update
{
    public class UpdateBookingCommandHandler(IBookingRepository bookingRepository) : IRequestHandler<UpdateBookingCommand, Result>
    {
        public async Task<Result> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var existingBooking = await bookingRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingBooking == null)
            {
                return Result.Failure(BookingErrors.BookingNotFound);
            }
            var hasOverlap = await bookingRepository.CheckBookingOverlapAsync(
                       existingBooking.Rooms.Select(r=>r.Id),
                       request.CheckInDate,
                       request.CheckOutDate,
                       cancellationToken);
            if (hasOverlap)
            {
                return Result.Failure(BookingErrors.BookingOverlap);
            }
            if (existingBooking.Status != BookingStatus.Pending)
            {
                return Result.Failure(BookingErrors.BookingNotPending);
            }
            var updatedBooking = request.ToBookingDomain();
            updatedBooking.UpdatedAt = DateTime.UtcNow;
            var result = await bookingRepository.UpdateBookingAsync(updatedBooking, cancellationToken);
            if (result == 0)
            {
                return Result.Failure(BookingErrors.BookingUpdateFailed);
            }
            return Result.Success();
        }
    }
}