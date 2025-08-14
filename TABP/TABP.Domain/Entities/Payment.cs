using TABP.Domain.Entities.Common;
using TABP.Domain.Enums;
namespace TABP.Domain.Entities
{
    public class Payment : SoftDeletable
    {
        public long Id { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
        public string? PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public PriceCurrency Currency { get; set; } = PriceCurrency.USD;
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public PaymentProvider Provider { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public string? FailureReason { get; set; }
        public string? ClientSecret { get; set; }
        public long BookingId { get; set; }
        public Booking Booking { get; set; } = null!;
    }
}