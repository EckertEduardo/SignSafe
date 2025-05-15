using MediatR;
using SignSafe.Domain.Enums.Users;
using System.ComponentModel.DataAnnotations;

namespace SignSafe.Application.Users.Commands.UpdateRole
{
    public class UpdateRolesCommand : IRequest
    {
        [Required]
        public long UserId { get; init; }

        [Required]
        public required List<UserRoles> Roles { get; init; }
    }
}
