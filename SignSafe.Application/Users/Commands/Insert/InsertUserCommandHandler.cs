using MediatR;
using SignSafe.Data.UoW;
using SignSafe.Domain.Entities;
using SignSafe.Domain.Enums.Users;

namespace SignSafe.Application.Users.Commands.Insert
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsertUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.InsertUserDto);
            var roles = new List<UserRoles> { UserRoles.Standard };

            user.EncryptUserPassword();
            user.UpdateRoles(roles);

            try
            {
                await _unitOfWork.UserRepository.Insert(user);
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }

            return;
        }
    }
}
