using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SignSafe.Application.Users.Commands.Delete
{
    public class DeleteUserCommand : IRequest
    {
        [Required]
        public long UserId { get; set; }
    }
}
