using MediatR;
using SignSafe.Application.Users.Dtos;
using SignSafe.Data.UoW;

namespace SignSafe.Application.Users.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.Get(request.UserId);
            if (user is null)
                return null;


            user.Update(
                name: request.UserDto.Name,
                email: request.UserDto.Email,
                birthDate: request.UserDto.BirthDate,
                phoneNumber: request.UserDto.PhoneNumber);

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Commit();

            return new UserDto(user);
        }
    }
}
