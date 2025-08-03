namespace TABP.Application.Bookings.Common
{
    public record PaymentInfo(
    string PaymentIntentId,
    string Status,
    string? ClientSecret  //Frontend needs this for 3D Secure
    );
}