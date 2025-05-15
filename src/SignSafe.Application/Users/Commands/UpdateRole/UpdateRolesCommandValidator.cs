using FluentValidation;
using SignSafe.Domain.Entities.Validations;

namespace SignSafe.Application.Users.Commands.UpdateRole
{
    public class UpdateRolesCommandValidator : AbstractValidator<UpdateRolesCommand>
    {
        public UpdateRolesCommandValidator()
        {
            GenericValidationRules
                .Id(RuleFor(x => x.UserId));

            UserValidationRules
                .UserRoles(RuleFor(x => x.Roles));
        }
    }
}
