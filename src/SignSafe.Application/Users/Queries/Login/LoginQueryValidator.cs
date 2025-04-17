using FluentValidation;
using SignSafe.Domain.Entities.Validations;

namespace SignSafe.Application.Users.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            GenericValidationRules
                .Email(RuleFor(x => x.Email));

            UserValidationRules
                .Password(RuleFor(x => x.Password));
        }
    }
}
