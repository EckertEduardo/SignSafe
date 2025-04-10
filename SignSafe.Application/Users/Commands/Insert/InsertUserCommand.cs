using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SignSafe.Application.Users.Commands.Insert
{
    public class InsertUserCommand : IRequest<InsertUserCommandResponse>
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public string Email { get; init; }
        [Required]
        public string Password { get; init; }

        public DateTime BirthDate { get; init; } = new DateTime();
        public string? PhoneNumber { get; init; }
    }
}
