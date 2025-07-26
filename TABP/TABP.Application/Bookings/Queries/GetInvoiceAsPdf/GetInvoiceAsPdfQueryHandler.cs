using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Application.Common;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
namespace TABP.Application.Bookings.Queries.GetById
{
    public class GetInvoiceAsPdfQueryHandler(
        IBookingRepository bookingRepository,
        IInvoiceTemplateBuilder invoiceTemplateBuilder,
        IPdfService pdfService) : IRequestHandler<GetInvoiceAsPdfQuery, Result<byte[]>>
    {
        public async Task<Result<byte[]>> Handle(GetInvoiceAsPdfQuery request, CancellationToken cancellationToken)
        {
            var booking = await bookingRepository.GetByIdAsync(request.BookingId, cancellationToken);
            if (booking is null)
                return Result<byte[]>.Failure(BookingErrors.BookingNotFound);
            var html = await invoiceTemplateBuilder.BuildHtmlAsync(booking, booking.Hotel, booking.User);
            var pdfBytes = await pdfService.GenerateBookingInvoicePdf(html);
            return Result<byte[]>.Success(pdfBytes);
        }
    }
}