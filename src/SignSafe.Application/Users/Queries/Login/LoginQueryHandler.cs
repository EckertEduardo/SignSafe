using MediatR;
using Microsoft.AspNet.Identity;
using SignSafe.Application.Auth;
using SignSafe.Infrastructure.UoW;

namespace SignSafe.Application.Users.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginQueryResponse?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public LoginQueryHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        public async Task<LoginQueryResponse?> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(request.Email);

            if (user != null)
            {
                var result = user.VerifyUserPassword(request.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    var token = _jwtService.GenerateToken(user);
                    return new LoginQueryResponse
                    {
                        JwtToken = token,
                        ExpiresIn = _jwtService.ConvertToken(token).ValidTo,
                        Enabled = user.Enabled
                    };
                }
            }

            return null;
        }
    }
}
