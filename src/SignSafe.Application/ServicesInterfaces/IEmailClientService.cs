using MimeKit;

namespace SignSafe.Application.ServicesInterfaces
{
    public interface IEmailClientService
    {
        Task SendEmailAsync(List<MailboxAddress> receiptEmails, BodyBuilder bodyBuilder, string subject);
    }
}
