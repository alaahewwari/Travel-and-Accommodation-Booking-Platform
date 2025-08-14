using System.Net.Mail;
namespace TABP.Domain.Models.Email
{
    public class EmailMessage
    {
        public string To { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public Attachment PdfAttachment { get; set; } = null!;
    }
}