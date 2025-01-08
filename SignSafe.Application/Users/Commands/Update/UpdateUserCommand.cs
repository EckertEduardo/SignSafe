using MediatR;
using SignSafe.Domain.Dtos.Users;

namespace SignSafe.Application.Users.Commands.Update
{
    public class UpdateUserCommand : IRequest
    {
        public required long UserId { get; set; }
        public required UserDto UserDto { get; set; }
    }
}
