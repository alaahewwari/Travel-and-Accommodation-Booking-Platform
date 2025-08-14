using System.Net.Mail;
using TABP.Domain.Entities;
using TABP.Domain.Models.Email;
namespace TABP.Domain.Interfaces.Services
{
    public interface IEmailMessageBuilder
    {
        EmailMessage BuildBookingConfirmation(Attachment attachment, User user);
        EmailMessage BuildRegistrationWelcome(User user);
    }
}