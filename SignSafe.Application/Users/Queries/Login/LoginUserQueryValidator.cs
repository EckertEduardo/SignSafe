using FluentValidation;

namespace SignSafe.Application.Users.Queries.Login
{

    public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
    {
        const int PASSWORD_MIN_LENGTH = 8;
        public LoginUserQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage($"Field '{nameof(LoginUserQuery.Email)}' is required")
                .EmailAddress().WithMessage($"Invalid {nameof(LoginUserQuery.Email)} format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage($"Field '{nameof(LoginUserQuery.Password)}' is required")
                .MinimumLength(PASSWORD_MIN_LENGTH).WithMessage($"Field '{nameof(LoginUserQuery.Password)}' must be at least {PASSWORD_MIN_LENGTH} characters");
        }
    }

}
