using SignSafe.Domain.Entities;

namespace SignSafe.Domain.Dtos.Users
{
    public class UserDto
    {
        public UserDto(User user)
        {
            Name = user.Name;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            BirthDate = user.BirthDate;
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
