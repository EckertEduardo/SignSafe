using FluentValidation;

namespace SignSafe.Application.Users.Queries.Login
{

    public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage($"Field '{nameof(LoginUserQuery.Email)}' is required")
                .EmailAddress().WithMessage($"Invalid {nameof(LoginUserQuery.Email)} format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage($"Field '{nameof(LoginUserQuery.Password)}' is required");

        }
    }

}
