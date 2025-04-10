using MediatR;
using SignSafe.Application.Users.Dtos;

namespace SignSafe.Application.Users.Queries.GetLogged
{
    public sealed class GetLoggedUserQuery : IRequest<UserDto>
    {
    }
}
