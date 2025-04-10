using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SignSafe.Application.Users.Commands.Update
{
    public class UpdateUserCommand : IRequest
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public string Name { get; init; }
        [Required]
        public string Email { get; init; }
        [Required]
        public DateTime BirthDate { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
