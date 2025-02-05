using MediatR;
using Microsoft.AspNet.Identity;
using SignSafe.Application.Auth;
using SignSafe.Data.UoW;

namespace SignSafe.Application.Users.Queries.Login
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public LoginUserQueryHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        public async Task<string?> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(request.Email);

            if (user != null)
            {
                var result = user.VerifyUserPassword(request.Password);
                if (result == PasswordVerificationResult.Success)
                    return _jwtService.GenerateToken(user);
            }

            return null;
        }
    }
}
