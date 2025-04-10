using MediatR;

namespace SignSafe.Application.Users.Commands.SendOtpEmailResetPassword
{
    public sealed class SendOtpEmailResetPasswordCommand : IRequest
    {
        public string Email { get; set; }
    }
}
