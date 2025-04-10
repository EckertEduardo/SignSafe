using MediatR;
using SignSafe.Application.Auth;
using SignSafe.Infrastructure.UoW;

namespace SignSafe.Application.Users.Queries.IsLogged
{
    public sealed class UserIsLoggedQueryHandler : IRequestHandler<UserIsLoggedQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        private readonly UserTokenInfo? _userTokenInfo;

        public UserIsLoggedQueryHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;

            _userTokenInfo = _jwtService.GetUserTokenInfo();
        }

        public async Task<bool> Handle(UserIsLoggedQuery request, CancellationToken cancellationToken)
        {
            var isLogged = false;

            if (_userTokenInfo is not null && await _unitOfWork.UserRepository.Get(_userTokenInfo.UserId) is not null)
            {
                isLogged = true;
            }

            return isLogged;
        }
    }
}
