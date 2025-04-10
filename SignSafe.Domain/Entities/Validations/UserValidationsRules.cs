using FluentValidation;
using SignSafe.Domain.Enums.Users;

namespace SignSafe.Domain.Entities.Validations
{
    public static class UserValidationRules
    {
        private static class UserValidations
        {
            public const int PASSWORD_MIN_LENGTH = 8;
            public const int PHONE_NUMBER_MIN_LENGTH = 8;
            public const int PHONE_NUMBER_MAX_LENGTH = 20;
            public static readonly DateTime BIRTH_DATE_MIN = new(year: 1900, month: 01, day: 01);
            public static readonly DateTime BIRTH_DATE_MAX = DateTime.UtcNow;
        }

        public static IRuleBuilderOptions<T, string> Password<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().NotEmpty().WithMessage("Field 'Password' is required")
                .MinimumLength(UserValidations.PASSWORD_MIN_LENGTH)
                .WithMessage($"Field 'Password' must be at least {UserValidations.PASSWORD_MIN_LENGTH} characters");
        }

        public static IRuleBuilderOptions<T, DateTime> BirthDate<T>(IRuleBuilder<T, DateTime> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().NotEmpty()
                    .WithMessage($"Field 'Birthdate' is required")
                .InclusiveBetween(UserValidations.BIRTH_DATE_MIN, UserValidations.BIRTH_DATE_MAX)
                    .WithMessage($"Field 'Birthdate' must be between {UserValidations.BIRTH_DATE_MIN} and {UserValidations.BIRTH_DATE_MAX}");
        }

        public static IRuleBuilderOptions<T, string?> PhoneNumber<T>(IRuleBuilder<T, string?> ruleBuilder, Func<T, string?> phoneNumber)
        {
            return ruleBuilder
                .Matches(@"^[\d\s-()]+$").WithMessage($"Field 'PhoneNumber' must contain only numbers, spaces, or dashes")
                .Length(UserValidations.PHONE_NUMBER_MIN_LENGTH, UserValidations.PHONE_NUMBER_MAX_LENGTH)
                    .WithMessage($"Field 'PhoneNumber' must be between {UserValidations.PHONE_NUMBER_MIN_LENGTH} and {UserValidations.PHONE_NUMBER_MAX_LENGTH} characters")
                .When(x => !string.IsNullOrWhiteSpace(phoneNumber(x)));
        }

        public static IRuleBuilderOptions<T, List<UserRoles>> UserRoles<T>(IRuleBuilder<T, List<UserRoles>> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage($"Field 'UserRoles' is required");
        }
    }
}
