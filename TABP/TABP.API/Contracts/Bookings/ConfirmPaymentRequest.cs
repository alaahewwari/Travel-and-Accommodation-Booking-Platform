namespace TABP.API.Contracts.Bookings
{
    public record ConfirmPaymentRequest(
           long BookingId,
           string PaymentIntentId
       );
}