using FluentValidation;
using SignSafe.Domain.Entities.Validations;

namespace SignSafe.Application.Users.Commands.SendOtpEmailResetPassword
{
    public sealed class SendOtpEmailResetPasswordCommandValidator : AbstractValidator<SendOtpEmailResetPasswordCommand>
    {
        public SendOtpEmailResetPasswordCommandValidator()
        {
            GenericValidationRules
                .Email(RuleFor(x => x.Email));
        }
    }
}
