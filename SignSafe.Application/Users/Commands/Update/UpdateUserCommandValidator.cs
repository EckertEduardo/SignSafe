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

            RuleFor(x => x.UpdateUserDto.Name)
                  .NotNull().NotEmpty().WithMessage($"Field '{nameof(UserDto.Name)}' is required");

            RuleFor(x => x.UpdateUserDto.Email)
                .NotNull().NotEmpty().WithMessage($"Field '{nameof(UserDto.Email)}' is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.UpdateUserDto.BirthDate)
                .NotNull().NotEmpty().WithMessage($"Field '{nameof(UserDto.BirthDate)}' is required")
                .InclusiveBetween(UserValidations.BIRTH_DATE_MIN, UserValidations.BIRTH_DATE_MAX).WithMessage($"Field '{nameof(UserDto.BirthDate)}' must be between {UserValidations.BIRTH_DATE_MIN} and {UserValidations.BIRTH_DATE_MAX}");

            RuleFor(x => x.UpdateUserDto.PhoneNumber)
                .Matches(@"^[\d\s-]+$").WithMessage($"Field '{nameof(UpdateUserCommand.UpdateUserDto.PhoneNumber)}' must contain only numbers, spaces, or dashes")
                .Length(UserValidations.PHONE_NUMBER_MIN_LENGTH, UserValidations.PHONE_NUMBER_MAX_LENGTH).WithMessage($"Field '{nameof(UpdateUserCommand.UpdateUserDto.PhoneNumber)}' must be between {UserValidations.PHONE_NUMBER_MIN_LENGTH} and {UserValidations.PHONE_NUMBER_MAX_LENGTH} characters")
                .When(x => !string.IsNullOrEmpty(x.UpdateUserDto.PhoneNumber));

        }
    }
}
