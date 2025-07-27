using System.Net.Mail;
namespace TABP.Domain.Models.Email
{
    public class EmailMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Attachment PdfAttachment { get; set; }
    }
}