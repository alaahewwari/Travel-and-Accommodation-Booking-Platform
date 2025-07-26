using Scriban;
using TABP.Domain.Entities;
using TABP.Domain.Entities.Views;
using TABP.Domain.Interfaces.Services;
namespace TABP.Infrastructure.Services
{
    public class InvoiceTemplateBuilder : IInvoiceTemplateBuilder
    {
        public async Task<string> BuildHtmlAsync(Booking booking, Hotel hotel, User user)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var templatePath = Path.Combine(basePath, "Templates", "InvoiceTemplate.html");
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException("Invoice template file not found.", templatePath);
            }
            var templateText = await File.ReadAllTextAsync(templatePath);
            var template = Template.Parse(templateText);
            var viewModel = new InvoiceViewModel
            {
                HotelName = hotel.Name,
                InvoiceNumber = booking.Invoice!.InvoiceNumber,
                IssueDate = booking.Invoice.IssueDate.ToString("yyyy-MM-dd"),
                TotalAmount = booking.Invoice.TotalAmount,
                Status = booking.Invoice.Status.ToString(),
                BookingCustomerName = user.FirstName + " " + user.LastName,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate
            };
            var result = template.Render(viewModel, member => member.Name);
            return result;
        }
    }
}