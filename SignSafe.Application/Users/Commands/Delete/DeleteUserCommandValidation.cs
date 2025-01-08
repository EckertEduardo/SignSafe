using FluentValidation;

namespace SignSafe.Application.Users.Commands.Delete
{
    public class DeleteUserCommandValidation : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidation()
        {
            RuleFor(x => x.UserId)
                 .GreaterThan(0);
        }
    }
}
