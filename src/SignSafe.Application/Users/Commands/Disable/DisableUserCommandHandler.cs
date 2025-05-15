using MediatR;
using SignSafe.Domain.Exceptions;
using SignSafe.Infrastructure.UoW;

namespace SignSafe.Application.Users.Commands.Disable
{
    public class DisableUserCommandHandler : IRequestHandler<DisableUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DisableUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(DisableUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.Get(request.UserId)
                ?? throw new NotFoundException("UserId", request.UserId);

            user.Disable();

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Commit();
        }
    }
}
