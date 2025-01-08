using MediatR;

namespace SignSafe.Application.Users.Commands.Delete
{
    public class DeleteUserCommand : IRequest
    {
        public long UserId { get; set; }
    }
}
