using MediatR;
using SignSafe.Domain.Dtos.Users;

namespace SignSafe.Application.Users.Queries.Get
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public long UserId { get; set; }
    }
}
