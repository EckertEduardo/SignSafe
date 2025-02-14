using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SignSafe.Application.Users.Dtos
{
    public record InsertUserDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public string Email { get; init; }
        [Required]
        public string Password { get; init; }
        [Required]
        public DateTime BirthDate { get; init; }
        public string? PhoneNumber { get; init; }

        [JsonConstructor]
        public InsertUserDto(string name, string email, string password, DateTime birthDate, string? phoneNumber = null)
        {
            Name = name;
            Email = email;
            Password = password;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
        }
    }
}
