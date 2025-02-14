using SignSafe.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SignSafe.Application.Users.Dtos
{
    public record UserDto
    {
        [JsonConstructor]
        public UserDto(string name, string email, DateTime birthDate, string? phoneNumber = null)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
        }

        public UserDto(User user)
        {
            Name = user.Name;
            Email = user.Email;
            BirthDate = user.BirthDate;
            PhoneNumber = user.PhoneNumber;
        }

        [Required]
        public string Name { get; init; }
        [Required]
        public string Email { get; init; }
        [Required]
        public DateTime BirthDate { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
