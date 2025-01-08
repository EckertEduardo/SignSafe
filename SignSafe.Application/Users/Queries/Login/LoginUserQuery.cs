using MediatR;

namespace SignSafe.Application.Users.Queries.Login
{
    public class LoginUserQuery : IRequest<string>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
