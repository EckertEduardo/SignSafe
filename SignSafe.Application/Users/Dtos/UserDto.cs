using SignSafe.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SignSafe.Application.Users.Dtos
{
    public record UserDto
    {
        [Required]
        public long Id { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        public string Email { get; init; }
        [Required]
        public DateTime Birthdate { get; init; }

        [Required]
        public bool VerifiedAccount { get; init; }
        public string? PhoneNumber { get; init; }

        [JsonConstructor]
        public UserDto(string name, string email, DateTime birthdate, string? phoneNumber = null)
        {
            Name = name;
            Email = email;
            Birthdate = birthdate;
            PhoneNumber = phoneNumber;
        }

        public UserDto(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Birthdate = user.BirthDate;
            PhoneNumber = user.PhoneNumber;
            VerifiedAccount = user.VerifiedAccount;
        }
    }
}
