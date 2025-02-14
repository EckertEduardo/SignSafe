using MediatR;
using SignSafe.Infrastructure.UoW;

namespace SignSafe.Application.Users.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.UserRepository.Delete(request.UserId);
            await _unitOfWork.Commit();
        }
    }
}
