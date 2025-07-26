using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Application.Bookings.Mapper;
using TABP.Application.Common;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Bookings.Queries.GetById
{
    public class GetBookingByIdQueryHandler(IBookingRepository bookingRespository) : IRequestHandler<GetBookingByIdQuery, Result<BookingResponse>>
    {
        public async Task<Result<BookingResponse>> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            var booking = await bookingRespository.GetByIdAsync(request.Id, cancellationToken);
            if (booking is null)
            {
                return Result<BookingResponse>.Failure(BookingErrors.BookingNotFound);
            }
            var response = booking.ToBookingResponse();
            return Result<BookingResponse>.Success(response);
        }
    }
}