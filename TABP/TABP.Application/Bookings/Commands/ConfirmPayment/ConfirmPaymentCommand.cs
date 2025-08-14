using MediatR;
using TABP.Application.Common;
namespace TABP.Application.Bookings.Commands.ConfirmPayment
{
    public record ConfirmPaymentCommand(
        long BookingId,
        string PaymentIntentId
    ): IRequest<Result>;
}