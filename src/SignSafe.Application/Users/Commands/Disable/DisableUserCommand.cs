using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SignSafe.Application.Users.Commands.Disable
{
    public class DisableUserCommand : IRequest
    {
        [Required]
        public long UserId { get; set; }
    }
}
