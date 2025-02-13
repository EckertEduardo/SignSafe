using FluentValidation;

namespace SignSafe.Application.Users.Queries.Get
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage($"Field '{nameof(GetUserQuery.UserId)}' is required")
                .GreaterThan(0).WithMessage($"Field '{nameof(GetUserQuery.UserId)} must be greater than 0");
        }
    }
}
