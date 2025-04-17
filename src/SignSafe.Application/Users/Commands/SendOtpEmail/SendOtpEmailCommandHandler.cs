using MediatR;
using MimeKit;
using SignSafe.Application.Auth;
using SignSafe.Application.ServicesInterfaces;
using SignSafe.Domain.EmailTemplates;

namespace SignSafe.Application.Users.Commands.SendOtpEmail
{
    public sealed class SendOtpEmailCommandHandler : IRequestHandler<SendOtpEmailCommand>
    {
        private readonly IEmailClientService _emailClientService;
        private readonly IOtpService _otpService;
        private readonly IJwtService _jwtService;

        private readonly UserTokenInfo? _userTokenInfo;

        public SendOtpEmailCommandHandler(IEmailClientService emailClientService, IOtpService otpService, IJwtService jwtService)
        {
            _emailClientService = emailClientService ?? throw new ArgumentNullException(nameof(emailClientService));
            _otpService = otpService ?? throw new ArgumentNullException(nameof(otpService));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));

            _userTokenInfo = _jwtService.GetUserTokenInfo();
        }

        public async Task Handle(SendOtpEmailCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(_userTokenInfo);

            string otpVerificationCode = await _otpService.UpdateUserOtpVerificationCode();

            var subject = "Otp Verification Code";

            var receiptEmails = new List<MailboxAddress>
            {
                new MailboxAddress(string.Empty,_userTokenInfo.Email)
            };

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = EmailTemplates.OTP
                        .Replace("{{email}}", receiptEmails.First().Address)
                        .Replace("{{otp}}", otpVerificationCode.ToString())
            };

            await _emailClientService.SendEmailAsync(receiptEmails, bodyBuilder, subject);
        }
    }
}
