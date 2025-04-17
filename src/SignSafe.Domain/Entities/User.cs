using Microsoft.AspNet.Identity;
using SignSafe.Domain.Enums.Users;
using SignSafe.Domain.Extensions;

namespace SignSafe.Domain.Entities
{
    public class User : Base
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? Password { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Roles { get; private set; } = UserRoles.Standard.GetDescription();
        public bool VerifiedAccount { get; private set; }
        public string? OtpVerificationCode { get; private set; }
        public DateTime? OtpVerificationCodeExpiration { get; private set; }
        public string? PhoneNumber { get; private set; }

        public User(string name, string email, string password, DateTime birthDate, string? phoneNumber)
        {
            Name = name;
            Email = email;
            Password = EncryptUserPassword(password);
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
        }

        private User() { }

        public void Update(string name, string email, DateTime birthDate, string? phoneNumber = null)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
        }

        public void UpdateRoles(List<UserRoles> roles)
        {
            Roles = string.Join(",", roles.Distinct());
        }

        public void UpdatePassword(string otpVerificationCode, string password)
        {
            if (OtpVerificationCodeIsValid(otpVerificationCode))
            {
                var hasher = new PasswordHasher();
                Password = hasher.HashPassword(password);
            }
        }

        public PasswordVerificationResult VerifyUserPassword(string providedPassword)
        {
            var hasher = new PasswordHasher();
            return hasher.VerifyHashedPassword(Password, providedPassword);
        }

        public void SetOtpInfo(string otpVerificationCode)
        {
            OtpVerificationCode = otpVerificationCode;
            OtpVerificationCodeExpiration = DateTime.UtcNow.AddMinutes(10);
        }

        public bool OtpVerificationCodeIsValid(string otpVerificationCode)
        {
            return string.Equals(OtpVerificationCode, otpVerificationCode, StringComparison.OrdinalIgnoreCase)
                 && OtpVerificationCodeExpiration >= DateTime.UtcNow;
        }

        public void VerifyAccount(string otpVerificationCode)
        {
            if (OtpVerificationCodeIsValid(otpVerificationCode))
            {
                VerifiedAccount = true;
            }
        }

        public string EncryptUserPassword(string password)
        {
            var hasher = new PasswordHasher();
            return hasher.HashPassword(password);
        }

    }
}
