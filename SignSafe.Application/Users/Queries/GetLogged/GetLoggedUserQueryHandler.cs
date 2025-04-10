using MediatR;
using SignSafe.Application.Auth;
using SignSafe.Application.Users.Dtos;
using SignSafe.Domain.Exceptions;
using SignSafe.Infrastructure.UoW;

namespace SignSafe.Application.Users.Queries.GetLogged
{
    public sealed class GetLoggedUserQueryHandler : IRequestHandler<GetLoggedUserQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        private readonly UserTokenInfo? _userTokenInfo;

        public GetLoggedUserQueryHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));

            _userTokenInfo = _jwtService.GetUserTokenInfo();
        }

        public async Task<UserDto> Handle(GetLoggedUserQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(_userTokenInfo);

            var user = await _unitOfWork.UserRepository.Get(_userTokenInfo.UserId)
                ?? throw new NotFoundException("UserTokenInfo", "UserId");

            return new UserDto(user);
        }
    }
}
