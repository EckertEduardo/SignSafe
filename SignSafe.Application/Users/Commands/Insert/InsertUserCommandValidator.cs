using FluentValidation;
using SignSafe.Application.Users.Dtos;
using SignSafe.Application.Users.Queries.Login;
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
            .MinimumLength(UserValidationFields.PASSWORD_MIN_LENGTH).WithMessage($"Field '{nameof(LoginUserQuery.Password)}' must be at least {UserValidationFields.PASSWORD_MIN_LENGTH} characters");

            RuleFor(x => x.InsertUserDto.BirthDate)
                .NotNull().NotEmpty().WithMessage($"Field '{nameof(InsertUserDto.BirthDate)}' is required")
                .InclusiveBetween(UserValidationFields.BIRTH_DATE_MIN, UserValidationFields.BIRTH_DATE_MAX).WithMessage($"Field '{nameof(InsertUserDto.BirthDate)}' must be between {UserValidationFields.BIRTH_DATE_MIN} and {UserValidationFields.BIRTH_DATE_MAX}");

            RuleFor(x => x.InsertUserDto.PhoneNumber)
                .Matches(@"^[\d\s-]+$").WithMessage($"Field '{nameof(InsertUserCommand.InsertUserDto.PhoneNumber)}' must contain only numbers, spaces, or dashes")
                .Length(UserValidationFields.PHONE_NUMBER_MIN_LENGTH, UserValidationFields.PHONE_NUMBER_MAX_LENGTH).WithMessage($"Field '{nameof(InsertUserCommand.InsertUserDto.PhoneNumber)}' must be between {UserValidationFields.PHONE_NUMBER_MIN_LENGTH} and {UserValidationFields.PHONE_NUMBER_MAX_LENGTH} characters")
                .When(x => !string.IsNullOrEmpty(x.InsertUserDto.PhoneNumber));

        }
    }
}
