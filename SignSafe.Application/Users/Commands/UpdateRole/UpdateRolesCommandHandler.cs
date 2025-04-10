using MediatR;
using SignSafe.Domain.Exceptions;
using SignSafe.Infrastructure.UoW;

namespace SignSafe.Application.Users.Commands.UpdateRole
{
    public class UpdateRolesCommandHandler : IRequestHandler<UpdateRolesCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRolesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(UpdateRolesCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.Get(request.UserId)
                ?? throw new NotFoundException("UserId", request.UserId);

            user.UpdateRoles(request.UserRoles);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Commit();
        }
    }
}
