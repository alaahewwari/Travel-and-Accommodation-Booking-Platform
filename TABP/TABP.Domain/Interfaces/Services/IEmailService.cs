using TABP.Domain.Models.Email;
namespace TABP.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}