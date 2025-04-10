using FluentValidation;
using SignSafe.Domain.Entities.Validations;

namespace SignSafe.Application.Users.Commands.VerifyAccount
{
    public sealed class VerifyAccountCommandValidator : AbstractValidator<VerifyAccountCommand>
    {
        public VerifyAccountCommandValidator()
        {
            GenericValidationRules
                .OtpVerificationCode(RuleFor(x => x.OtpVerificationCode));
        }
    }
}
