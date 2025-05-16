using MediatR;
using SignSafe.Application.Auth;
using SignSafe.Domain.Exceptions;
using SignSafe.Infrastructure.UoW;

namespace SignSafe.Application.Users.Commands.UpdateRole
{
    public class UpdateRolesCommandHandler : IRequestHandler<UpdateRolesCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        private readonly UserTokenInfo? _userTokenInfo;
        public UpdateRolesCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));

            _userTokenInfo = _jwtService.GetUserTokenInfo();
        }

        public async Task Handle(UpdateRolesCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(_userTokenInfo);

            var user = await _unitOfWork.UserRepository.Get(request.UserId)
                ?? throw new NotFoundException("UserId", request.UserId);

            user.UpdateRoles(request.Roles);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Commit();

            if (user.Id == _userTokenInfo.UserId)
            {
                _jwtService.RefreshToken(user);
            }
        }
    }
}
