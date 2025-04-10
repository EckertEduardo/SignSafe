using FluentValidation;
using SignSafe.Domain.Entities.Validations;

namespace SignSafe.Application.Users.Commands.Insert
{
    public class InsertUserCommandValidator : AbstractValidator<InsertUserCommand>
    {
        public InsertUserCommandValidator()
        {
            GenericValidationRules
                .Required(RuleFor(x => x.Name));

            GenericValidationRules
                .Email(RuleFor(x => x.Email));

            UserValidationRules
                 .Password(RuleFor(x => x.Password));

            UserValidationRules
                .BirthDate(RuleFor(x => x.BirthDate));

            UserValidationRules
                .PhoneNumber(RuleFor(x => x.PhoneNumber), x => x.PhoneNumber);
        }
    }
}
