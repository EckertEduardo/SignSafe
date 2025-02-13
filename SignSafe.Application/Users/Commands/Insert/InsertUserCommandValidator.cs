using FluentValidation;
using SignSafe.Domain.Dtos.Users;

namespace SignSafe.Application.Users.Commands.Insert
{
    public class InsertUserCommandValidator : AbstractValidator<InsertUserCommand>
    {
        private readonly DateTime birthDateMin = new(year: 1900, month: 01, day: 01);
        private readonly DateTime birthDateMax = DateTime.UtcNow;
        const int PHONE_NUMBER_MIN_LENGTH = 8;
        const int PHONE_NUMBER_MAX_LENGTH = 20;
        public InsertUserCommandValidator()
        {
            RuleFor(x => x.InsertUserDto.Name)
                .NotNull().NotEmpty().WithMessage($"Field '{nameof(InsertUserDto.Name)}' is required");

            RuleFor(x => x.InsertUserDto.Email)
                .NotNull().NotEmpty().WithMessage($"Field '{nameof(InsertUserDto.Email)}' is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.InsertUserDto.Password)
                .NotNull().NotEmpty().WithMessage($"Field '{nameof(InsertUserDto.Password)}' is required");

            RuleFor(x => x.InsertUserDto.BirthDate)
                .NotNull().NotEmpty().WithMessage($"Field '{nameof(InsertUserDto.BirthDate)}' is required")
                .InclusiveBetween(birthDateMin, birthDateMax).WithMessage($"Field '{nameof(InsertUserDto.BirthDate)}' must be between {birthDateMin} and {birthDateMax}");

            RuleFor(x => x.InsertUserDto.PhoneNumber)
                .Matches(@"^[\d\s-]+$").WithMessage($"Field '{nameof(InsertUserCommand.InsertUserDto.PhoneNumber)}' must contain only numbers, spaces, or dashes")
                .Length(PHONE_NUMBER_MIN_LENGTH, PHONE_NUMBER_MAX_LENGTH).WithMessage($"Field '{nameof(InsertUserCommand.InsertUserDto.PhoneNumber)}' must be between {PHONE_NUMBER_MIN_LENGTH} and {PHONE_NUMBER_MAX_LENGTH} characters")
                .When(x => !string.IsNullOrEmpty(x.InsertUserDto.PhoneNumber));

        }
    }
}
