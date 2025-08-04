using TABP.Domain.Interfaces.Services;
using TABP.Domain.Models.Email;
namespace Application.IntegrationTests.Common.TestInfrastructure
{
    /// <summary>
    /// Test implementation of email service that captures sent emails for verification in tests.
    /// </summary>
    public class TestEmailService : IEmailService
    {
        public List<EmailMessage> SentEmails { get; } = new();
        public Task SendEmailAsync(EmailMessage emailMessage)
        {
            SentEmails.Add(emailMessage);
            return Task.CompletedTask;
        }
    }
}