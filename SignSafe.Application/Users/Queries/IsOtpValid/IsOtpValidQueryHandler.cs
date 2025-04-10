using MediatR;
using SignSafe.Application.Auth;
using SignSafe.Domain.Exceptions;
using SignSafe.Infrastructure.UoW;

namespace SignSafe.Application.Users.Queries.IsOtpValid
{
    public sealed class IsOtpValidQueryHandler : IRequestHandler<IsOtpValidQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public IsOtpValidQueryHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<bool> Handle(IsOtpValidQuery request, CancellationToken cancellationToken)
        {
            var userEmail = request.Email;
            ArgumentException.ThrowIfNullOrEmpty(nameof(userEmail));


            var user = await _unitOfWork.UserRepository.GetByEmail(userEmail!)
                ?? throw new NotFoundException("Email", userEmail!);

            return user.OtpVerificationCodeIsValid(request.OtpVerificationCode);
        }
    }
}
