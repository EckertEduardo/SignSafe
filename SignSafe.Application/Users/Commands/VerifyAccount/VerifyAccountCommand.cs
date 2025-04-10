using MediatR;

namespace SignSafe.Application.Users.Commands.VerifyAccount
{
    public sealed class VerifyAccountCommand : IRequest<bool>
    {
        public string OtpVerificationCode { get; set; } = string.Empty;
    }
}
