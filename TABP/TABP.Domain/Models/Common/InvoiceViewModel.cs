namespace TABP.Domain.Models.Common
{
    public class InvoiceViewModel
    {
        public string HotelName { get; set; } = null!;
        public string InvoiceNumber { get; set; } = null!;
        public string IssueDate { get; set; } = null!;
        public string TotalAmountFormatted => TotalAmount.ToString("0.00");
        public string Status { get; set; } = null!;
        public string BookingCustomerName { get; set; } = null!;
        public string CheckInDateFormatted => CheckInDate.ToString("yyyy-MM-dd");
        public string CheckOutDateFormatted => CheckOutDate.ToString("yyyy-MM-dd");
        public decimal TotalAmount { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}