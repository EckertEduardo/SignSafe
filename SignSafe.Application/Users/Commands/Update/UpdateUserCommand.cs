using MediatR;
using SignSafe.Application.Users.Dtos;
using System.ComponentModel.DataAnnotations;

namespace SignSafe.Application.Users.Commands.Update
{
    public class UpdateUserCommand : IRequest<UserDto?>
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public required UserDto UserDto { get; set; }
    }
}
