using MediatR;
using SignSafe.Domain.Exceptions;
using SignSafe.Infrastructure.UoW;

namespace SignSafe.Application.Users.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.Get(request.UserId)
                ?? throw new NotFoundException("UserId", request.UserId);

            user.Update(
                name: request.Name,
                email: request.Email,
                birthDate: request.BirthDate,
                phoneNumber: request.PhoneNumber);

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Commit();
        }
    }
}
