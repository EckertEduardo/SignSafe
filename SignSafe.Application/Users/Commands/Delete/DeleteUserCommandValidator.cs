using FluentValidation;
using SignSafe.Domain.Entities.Validations;

namespace SignSafe.Application.Users.Commands.Delete
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            GenericValidationRules
                .Id(RuleFor(x => x.UserId));
        }
    }
}
