using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Application.Common;
using TABP.Domain.Models.Common;
namespace TABP.Application.Bookings.Queries.GetById
{
    public record GetBookingsQuery(
         string? Filters,
         string? Sorts,
         int? Page,
         int? PageSize
        )
        : IRequest<Result<PagedResult<BookingResponse>>>;
}