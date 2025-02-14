using Microsoft.AspNet.Identity;
using SignSafe.Domain.Enums.Users;
using SignSafe.Domain.Extensions;

namespace SignSafe.Domain.Entities
{
    public class User : Base
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Roles { get; private set; } = UserRoles.Standard.GetDescription();
        public string? PhoneNumber { get; private set; }
        public User(string name, string email, string password, DateTime birthDate, string? phoneNumber)
        {
            Name = name;
            Email = email;
            Password = password;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
        }


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

        public PasswordVerificationResult VerifyUserPassword(string providedPassword)
        {
            var hasher = new PasswordHasher();
            return hasher.VerifyHashedPassword(Password, providedPassword);
        }

        public void EncryptUserPassword()
        {
            var hasher = new PasswordHasher();
            Password = hasher.HashPassword(Password);
        }

    }
}
