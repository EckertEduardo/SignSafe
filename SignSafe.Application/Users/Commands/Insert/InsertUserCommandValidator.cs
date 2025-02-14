using FluentValidation;
using SignSafe.Application.Users.Dtos;
using SignSafe.Domain.Entities.Validations;

namespace SignSafe.Application.Users.Commands.Insert
{
    public class InsertUserCommandValidator : AbstractValidator<InsertUserCommand>
    {
        public InsertUserCommandValidator()
        {
            RuleFor(x => x.InsertUserDto.Name)
                .NotNull().NotEmpty().WithMessage($"Field '{nameof(InsertUserDto.Name)}' is required");

            RuleFor(x => x.InsertUserDto.Email)
                .NotNull().NotEmpty().WithMessage($"Field '{nameof(InsertUserDto.Email)}' is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.InsertUserDto.Password)
                .NotNull().NotEmpty().WithMessage($"Field '{nameof(InsertUserDto.Password)}' is required")
            .MinimumLength(UserValidations.PASSWORD_MIN_LENGTH).WithMessage($"Field '{nameof(InsertUserDto.Password)}' must be at least {UserValidations.PASSWORD_MIN_LENGTH} characters");

            RuleFor(x => x.InsertUserDto.BirthDate)
                .NotNull().NotEmpty().WithMessage($"Field '{nameof(InsertUserDto.BirthDate)}' is required")
                .InclusiveBetween(UserValidations.BIRTH_DATE_MIN, UserValidations.BIRTH_DATE_MAX).WithMessage($"Field '{nameof(InsertUserDto.BirthDate)}' must be between {UserValidations.BIRTH_DATE_MIN} and {UserValidations.BIRTH_DATE_MAX}");

            RuleFor(x => x.InsertUserDto.PhoneNumber)
                .Matches(@"^[\d\s-]+$").WithMessage($"Field '{nameof(InsertUserCommand.InsertUserDto.PhoneNumber)}' must contain only numbers, spaces, or dashes")
                .Length(UserValidations.PHONE_NUMBER_MIN_LENGTH, UserValidations.PHONE_NUMBER_MAX_LENGTH).WithMessage($"Field '{nameof(InsertUserCommand.InsertUserDto.PhoneNumber)}' must be between {UserValidations.PHONE_NUMBER_MIN_LENGTH} and {UserValidations.PHONE_NUMBER_MAX_LENGTH} characters")
                .When(x => !string.IsNullOrEmpty(x.InsertUserDto.PhoneNumber));

        }
    }
}
