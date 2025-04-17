namespace SignSafe.Application.ServicesInterfaces
{
    public interface IOtpService
    {
        Task<string> UpdateUserOtpVerificationCode();
        Task<string> UpdateUserOtpVerificationCode(string email);
    }
}
