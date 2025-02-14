using MediatR;
using SignSafe.Domain.Exceptions;
using SignSafe.Infrastructure.UoW;

namespace SignSafe.Application.Users.Commands.UpdateRole
{
    public class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRolesCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserRolesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.Get(request.UserId)
                ?? throw new NotFoundException(nameof(UpdateUserRolesCommand.UserId), request.UserId);

            user.UpdateRoles(request.UserRoles);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Commit();
        }
    }
}
