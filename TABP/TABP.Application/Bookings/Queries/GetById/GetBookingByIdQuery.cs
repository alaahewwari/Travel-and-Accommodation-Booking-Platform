using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Application.Common;
namespace TABP.Application.Bookings.Queries.GetById
{
    public record GetBookingByIdQuery(long Id) : IRequest<Result<BookingResponse>>;
}