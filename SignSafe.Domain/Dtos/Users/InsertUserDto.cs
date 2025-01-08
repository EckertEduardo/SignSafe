namespace SignSafe.Domain.Dtos.Users
{
    public class InsertUserDto
    {
        public InsertUserDto(string name, string email, string password, string phoneNumber, DateTime birthDate)
        {
            Name = name;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
