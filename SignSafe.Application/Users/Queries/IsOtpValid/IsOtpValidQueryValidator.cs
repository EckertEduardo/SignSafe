using FluentValidation;
using SignSafe.Domain.Entities.Validations;

namespace SignSafe.Application.Users.Queries.IsOtpValid
{
    public sealed class IsOtpValidQueryValidator : AbstractValidator<IsOtpValidQuery>
    {
        public IsOtpValidQueryValidator()
        {
            GenericValidationRules
                .OtpVerificationCode(RuleFor(x => x.OtpVerificationCode));
        }
    }
}
