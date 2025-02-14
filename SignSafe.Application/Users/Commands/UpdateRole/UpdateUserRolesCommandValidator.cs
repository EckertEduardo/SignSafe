using FluentValidation;

namespace SignSafe.Application.Users.Commands.UpdateRole
{
    public class UpdateUserRolesCommandValidator : AbstractValidator<UpdateUserRolesCommand>
    {
        public UpdateUserRolesCommandValidator()
        {
            RuleFor(x => x.UserId)
            .NotNull().NotEmpty().WithMessage($"Field '{nameof(UpdateUserRolesCommand.UserId)}' is required");

            RuleFor(x => x.UserRoles)
                .NotEmpty().WithMessage($"Field '{nameof(UpdateUserRolesCommand.UserRoles)}' is required");
        }
    }
}
