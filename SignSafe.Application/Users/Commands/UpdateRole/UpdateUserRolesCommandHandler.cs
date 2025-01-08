using MediatR;
using SignSafe.Data.UoW;

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
            var user = await _unitOfWork.UserRepository.Get(request.UserId);
            if (user is null)
                return;

            user.UpdateRoles(request.UserRoles);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Commit();

            return;
        }
    }
}
