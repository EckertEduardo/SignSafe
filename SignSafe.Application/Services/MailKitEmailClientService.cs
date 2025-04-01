using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using SignSafe.Application.ServicesInterfaces;

namespace SignSafe.Application.Services
{
    public sealed class MailKitEmailClientService : IEmailClientService
    {
        const string PROVIDER = "MailKit";

        const string SMTP_SERVER_DEFAULT = "smtp.gmail.com";
        const int SMTP_PORT_DEFAULT = 587; // Use 465 for SSL or 587 for TLS
        const string EMAIL_NAME = "SignSafe Corp";

        private readonly IConfiguration _configuration;
        private readonly ILogger<MailKitEmailClientService> _logger;
        public MailKitEmailClientService(IConfiguration configuration, ILogger<MailKitEmailClientService> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SendEmailAsync(List<MailboxAddress> receiptEmails, BodyBuilder bodyBuilder, string subject)
        {
            _logger.LogInformation("{provider} - Initiating sending email", PROVIDER);
            var smtpSettings = _configuration.GetSection("EmailClient");

            if (string.IsNullOrEmpty(smtpSettings?["Email"]))
            {
                var errorMessage = "The section 'EmailClient' or 'EmailClient:Email' was not found";

                _logger.LogError("{provider} - Error sending email - {errorMessage}", PROVIDER, errorMessage);
                throw new ArgumentNullException("EmailClient:Email", errorMessage);
            }

            if (string.IsNullOrEmpty(smtpSettings?["Password"]))
            {
                var errorMessage = "The section 'EmailClient' or 'EmailClient:Password' was not found";

                _logger.LogError("{provider} - Error sending email - {errorMessage}", PROVIDER, errorMessage);
                throw new ArgumentNullException("EmailClient:Password", errorMessage);
            }

            string smtpServer = smtpSettings["SmtpServer"] ?? SMTP_SERVER_DEFAULT;
            int smtpPort = int.TryParse(smtpSettings["SmtpPort"], out int smtpPortValue) ? smtpPortValue : SMTP_PORT_DEFAULT;
            string fromEmail = smtpSettings["Email"]!;
            string fromPassword = smtpSettings["Password"]!;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(EMAIL_NAME, fromEmail));
            message.To.AddRange(receiptEmails);
            message.Subject = subject;

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                    await client.AuthenticateAsync(fromEmail, fromPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                    _logger.LogInformation("{provider} - Email sent successfuly", PROVIDER);
                    _logger.LogInformation("{provider} - Finishing sending email", PROVIDER);
                }
                catch (Exception ex)
                {
                    _logger.LogError("{provider} - Error sending email - {errorMessage}", PROVIDER, ex.Message);
                    throw;
                }
            }
        }
    }
}
