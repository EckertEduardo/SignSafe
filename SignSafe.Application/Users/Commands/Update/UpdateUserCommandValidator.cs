using FluentValidation;
using SignSafe.Application.Users.Dtos;
using SignSafe.Domain.Entities.Validations;

namespace SignSafe.Application.Users.Commands.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId)
            .NotNull().NotEmpty().WithMessage($"Field '{nameof(UpdateUserCommand.UserId)}' is required");

            RuleFor(x => x.UserDto.Name)
                  .NotNull().NotEmpty().WithMessage($"Field '{nameof(UserDto.Name)}' is required");

            RuleFor(x => x.UserDto.Email)
                .NotNull().NotEmpty().WithMessage($"Field '{nameof(UserDto.Email)}' is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.UserDto.BirthDate)
                .NotNull().NotEmpty().WithMessage($"Field '{nameof(UserDto.BirthDate)}' is required")
                .InclusiveBetween(UserValidationFields.BIRTH_DATE_MIN, UserValidationFields.BIRTH_DATE_MAX).WithMessage($"Field '{nameof(UserDto.BirthDate)}' must be between {UserValidationFields.BIRTH_DATE_MIN} and {UserValidationFields.BIRTH_DATE_MAX}");

            RuleFor(x => x.UserDto.PhoneNumber)
                .Matches(@"^[\d\s-]+$").WithMessage($"Field '{nameof(UpdateUserCommand.UserDto.PhoneNumber)}' must contain only numbers, spaces, or dashes")
                .Length(UserValidationFields.PHONE_NUMBER_MIN_LENGTH, UserValidationFields.PHONE_NUMBER_MAX_LENGTH).WithMessage($"Field '{nameof(UpdateUserCommand.UserDto.PhoneNumber)}' must be between {UserValidationFields.PHONE_NUMBER_MIN_LENGTH} and {UserValidationFields.PHONE_NUMBER_MAX_LENGTH} characters")
                .When(x => !string.IsNullOrEmpty(x.UserDto.PhoneNumber));

        }
    }
}
