namespace TABP.Domain.Interfaces.Services
{
    public interface IPdfService
    {
        Task<byte[]> GenerateBookingInvoicePdf(string Html);
    }
}