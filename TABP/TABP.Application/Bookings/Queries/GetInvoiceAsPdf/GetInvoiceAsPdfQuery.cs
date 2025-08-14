using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Bookings.Queries.GetById
{
    public record GetInvoiceAsPdfQuery(long BookingId) : IRequest<Result<byte[]>>;
}