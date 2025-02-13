using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SignSafe.Application.Users.Commands.Delete
{
    public class DeleteUserCommand : IRequest
    {
        [Required]
        public required long UserId { get; set; }
    }
}
