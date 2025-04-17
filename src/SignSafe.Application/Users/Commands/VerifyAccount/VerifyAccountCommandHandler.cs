using MediatR;
using SignSafe.Application.Auth;
using SignSafe.Domain.Exceptions;
using SignSafe.Infrastructure.UoW;

namespace SignSafe.Application.Users.Commands.VerifyAccount
{
    public sealed class VerifyAccountCommandHandler : IRequestHandler<VerifyAccountCommand, bool>
    {
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserTokenInfo? _userTokenInfo;
        public VerifyAccountCommandHandler(IJwtService jwtService, IUnitOfWork unitOfWork)
        {
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            _userTokenInfo = _jwtService.GetUserTokenInfo();
        }
        public async Task<bool> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(_userTokenInfo);

            var user = await _unitOfWork.UserRepository.Get(_userTokenInfo.UserId)
                ?? throw new NotFoundException("UserTokenInfo", "UserId");

            user!.VerifyAccount(request.OtpVerificationCode);

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Commit();

            return user.VerifiedAccount;
        }
    }
}
