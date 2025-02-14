using MediatR;
using SignSafe.Domain.Dtos.Users;
using System.ComponentModel.DataAnnotations;

namespace SignSafe.Application.Users.Commands.Update
{
    public class UpdateUserCommand : IRequest<UserDto?>
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public UserDto UserDtoX { get; set; }
    }
}
