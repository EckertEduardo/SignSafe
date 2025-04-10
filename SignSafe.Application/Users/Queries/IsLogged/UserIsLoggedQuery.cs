using MediatR;

namespace SignSafe.Application.Users.Queries.IsLogged
{
    public sealed class UserIsLoggedQuery : IRequest<bool>
    {
    }
}
