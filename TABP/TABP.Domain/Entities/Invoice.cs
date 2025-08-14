using TABP.Domain.Entities.Common;
using TABP.Domain.Enums;
namespace TABP.Domain.Entities
{
    public class Invoice : SoftDeletable
    {
        public long Id { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public DateTime IssueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentStatus Status { get; set; } 
        public long BookingId { get; set; }
        public Booking Booking { get; set; } = null!;
    }
}