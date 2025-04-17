using FluentValidation;
using SignSafe.Domain.Entities.Validations;

namespace SignSafe.Application.Users.Commands.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            GenericValidationRules
                .Id(RuleFor(x => x.UserId));

            GenericValidationRules
               .Required(RuleFor(x => x.Name));

            GenericValidationRules
                .Email(RuleFor(x => x.Email));

            UserValidationRules
                .BirthDate(RuleFor(x => x.BirthDate));

            UserValidationRules
                .PhoneNumber(RuleFor(x => x.PhoneNumber), x => x.PhoneNumber);
        }
    }
}
