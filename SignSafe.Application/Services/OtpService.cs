using Microsoft.Extensions.Logging;
using SignSafe.Application.Auth;
using SignSafe.Application.ServicesInterfaces;
using SignSafe.Domain.Exceptions;
using SignSafe.Infrastructure.UoW;
using System.Security.Cryptography;

namespace SignSafe.Application.Services
{
    public sealed class OtpService : IOtpService
    {
        private readonly ILogger<OtpService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        private readonly UserTokenInfo? _userTokenInfo;

        public OtpService(ILogger<OtpService> logger, IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));

            _userTokenInfo = _jwtService.GetUserTokenInfo();
        }
        public async Task<string> UpdateUserOtpVerificationCode()
        {
            _logger.LogInformation("Initiating UpdateUserOtpVerificationCode from service - {service}", nameof(OtpService));

            ArgumentNullException.ThrowIfNull(_userTokenInfo);

            var otpVerificationCode = GenerateOtpVerificationCode();
            var user = await _unitOfWork.UserRepository.Get(_userTokenInfo.UserId)
                ?? throw new NotFoundException("UserId", _userTokenInfo.UserId);

            user.SetOtpInfo(otpVerificationCode);

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Commit();

            _logger.LogInformation("Finishing UpdateUserOtpVerificationCode from service - {service}", nameof(OtpService));
            return otpVerificationCode;
        }

        public async Task<string> UpdateUserOtpVerificationCode(string email)
        {
            _logger.LogInformation("Initiating UpdateUserOtpVerificationCode from service - {service}", nameof(OtpService));

            var otpVerificationCode = GenerateOtpVerificationCode();
            var user = await _unitOfWork.UserRepository.GetByEmail(email)
                ?? throw new NotFoundException("UserEmail", email);

            user.SetOtpInfo(otpVerificationCode);

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Commit();

            _logger.LogInformation("Finishing UpdateUserOtpVerificationCode from service - {service}", nameof(OtpService));
            return otpVerificationCode;
        }

        private string GenerateOtpVerificationCode()
        {
            const int otpLength = 6;
            const string digits = "0123456789";
            char[] otp = new char[otpLength];
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[otpLength];
                rng.GetBytes(randomBytes);
                for (int i = 0; i < otpLength; i++)
                {
                    otp[i] = digits[randomBytes[i] % digits.Length];
                }
            }
            return new string(otp);
        }
    }
}
