using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SignSafe.Application.Users.Commands.Enable
{
    public class EnableUserCommand : IRequest
    {
        [Required]
        public long UserId { get; set; }
    }
}
