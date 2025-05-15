using MediatR;
using SignSafe.Domain.Exceptions;
using SignSafe.Infrastructure.UoW;

namespace SignSafe.Application.Users.Commands.Enable
{
    public class EnableUserCommandHandler : IRequestHandler<EnableUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnableUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(EnableUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.Get(request.UserId)
                ?? throw new NotFoundException("UserId", request.UserId);

            user.Enable();

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Commit();
        }
    }
}
