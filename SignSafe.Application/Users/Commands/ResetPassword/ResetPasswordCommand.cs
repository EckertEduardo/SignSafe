using MediatR;

namespace SignSafe.Application.Users.Commands.ResetPassword
{
    public sealed class ResetPasswordCommand : IRequest
    {
        public string Email { get; set; } = string.Empty;
        public string OtpVerificationCode { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
