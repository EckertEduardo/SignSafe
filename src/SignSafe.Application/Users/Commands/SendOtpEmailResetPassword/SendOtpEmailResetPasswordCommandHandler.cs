using MediatR;
using MimeKit;
using SignSafe.Application.ServicesInterfaces;
using SignSafe.Domain.EmailTemplates;

namespace SignSafe.Application.Users.Commands.SendOtpEmailResetPassword
{
    public sealed class SendOtpEmailResetPasswordCommandHandler : IRequestHandler<SendOtpEmailResetPasswordCommand>
    {
        private readonly IEmailClientService _emailClientService;
        private readonly IOtpService _otpService;

        public SendOtpEmailResetPasswordCommandHandler(IEmailClientService emailClientService, IOtpService otpService)
        {
            _emailClientService = emailClientService ?? throw new ArgumentNullException(nameof(emailClientService));
            _otpService = otpService ?? throw new ArgumentNullException(nameof(otpService));
        }

        public async Task Handle(SendOtpEmailResetPasswordCommand request, CancellationToken cancellationToken)
        {
            string otpVerificationCode = await _otpService.UpdateUserOtpVerificationCode(request.Email);

            var subject = "Otp Verification Code to Reset Password";

            var receiptEmails = new List<MailboxAddress>
            {
                new MailboxAddress(string.Empty,request.Email)
            };

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = EmailTemplates.PASSWORD_RESET
                        .Replace("{{email}}", receiptEmails.First().Address)
                        .Replace("{{otp}}", otpVerificationCode.ToString())
            };

            await _emailClientService.SendEmailAsync(receiptEmails, bodyBuilder, subject);
        }
    }
}
