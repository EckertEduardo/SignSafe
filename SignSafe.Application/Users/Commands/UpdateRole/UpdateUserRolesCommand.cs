using MediatR;
using SignSafe.Domain.Enums.Users;

namespace SignSafe.Application.Users.Commands.UpdateRole
{
    public class UpdateUserRolesCommand : IRequest
    {
        public required long UserId { get; set; }
        public required List<UserRoles> UserRoles { get; set; }
    }
}
