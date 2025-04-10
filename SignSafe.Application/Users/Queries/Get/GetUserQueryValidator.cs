using FluentValidation;
using SignSafe.Domain.Entities.Validations;

namespace SignSafe.Application.Users.Queries.Get
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            GenericValidationRules
                .Id(RuleFor(x => x.UserId));
        }
    }
}
