using FluentValidation;

namespace SignSafe.Application.Users.Commands.Delete
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage($"Field '{nameof(DeleteUserCommand.UserId)}' is required")
                .GreaterThan(0).WithMessage($"Field '{nameof(DeleteUserCommand.UserId)}' must be greater than 0");
        }
    }
}
