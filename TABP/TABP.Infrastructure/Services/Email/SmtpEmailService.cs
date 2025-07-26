using Microsoft.Extensions.Options;
using System.Net.Mail;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.Models.Email;
using TABP.Infrastructure.Configurations;
namespace TABP.Infrastructure.Services.Email
{
    public class SmtpEmailService(IOptionsMonitor<SmtpSettings> settings) : IEmailService
    {
        public async Task SendEmailAsync(EmailMessage message)
        {
            using var mailMessage = new MailMessage
            {
                From = new MailAddress(settings.CurrentValue.SenderEmail),
                To = { message.To },
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            };
            mailMessage.Attachments.Add(message.PdfAttachment);
            using var smtpClient = new SmtpClient(settings.CurrentValue.Host, settings.CurrentValue.Port)
            {
                Credentials = new System.Net.NetworkCredential(settings.CurrentValue.User, settings.CurrentValue.Password),
                EnableSsl = true
            };
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}