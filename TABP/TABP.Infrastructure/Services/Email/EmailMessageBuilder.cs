using System.Net.Mail;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.Models.Email;
namespace TABP.Infrastructure.Services.Email
{
    public class EmailMessageBuilder : IEmailMessageBuilder
    {
        public EmailMessage BuildBookingConfirmation(Attachment attachment, User user)
        {
            var subject = "Booking Confirmation";
            return new EmailMessage
            {
                To = user.Email,
                Subject = subject,
                Body = $"""
                    Dear {user.FirstName +" "+ user.LastName},
                    Thank you for your booking!
                    Your booking has been confirmed and the invoice is attached to this email as a PDF document.
                    If you have any questions or need further assistance, feel free to reply to this email.
                    Best regards,  
                    TABP Team
                    """,
                PdfAttachment = attachment
            };
        }
        public EmailMessage BuildRegistrationWelcome(User user)
        {
            var subject = "Welcome to TABP!";
            var body = @$"""
            Hi {user.Username},
            Thank you for registering at TABP.
            We’re excited to have you on board.
            — The TABP Team
            """;
            return new EmailMessage
            {
                To = user.Email,
                Subject = subject,
                Body = body
            };
        }
    }
}
