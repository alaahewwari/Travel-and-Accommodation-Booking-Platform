using TABP.Domain.Entities;
namespace TABP.Domain.Interfaces.Services
{
    public interface IInvoiceTemplateBuilder
    {
        Task<string> BuildHtmlAsync(Booking booking, Hotel hotel, User user);
    }
}