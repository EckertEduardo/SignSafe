using FluentValidation;

namespace SignSafe.Domain.Entities.Validations
{
    public static class GenericValidationRules
    {
        public static IRuleBuilderOptions<T, long> Id<T>(IRuleBuilder<T, long> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("Field 'Id' is required")
                .GreaterThan(0).WithMessage("Field 'Id' must be greater than 0");
        }

        public static IRuleBuilderOptions<T, string> Required<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().NotEmpty().WithMessage("Field 'Email' is required");
        }

        public static IRuleBuilderOptions<T, string> Email<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().NotEmpty().WithMessage("Field 'Email' is required")
                .EmailAddress().WithMessage("Invalid email format");
        }

        public static IRuleBuilderOptions<T, string> OtpVerificationCode<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().NotEmpty().WithMessage("Field 'OtpVerificationCode' is required")
                .Length(6, 6).WithMessage($"Field 'OtpVerificationCode' must contains exactly 6 digits");
        }
    }
}
