using MediatR;
using SignSafe.Domain.Enums.Users;
using System.ComponentModel.DataAnnotations;

namespace SignSafe.Application.Users.Commands.UpdateRole
{
    public class UpdateUserRolesCommand : IRequest
    {
        [Required]
        public long UserId { get; init; }

        [Required]
        public required List<UserRoles> UserRoles { get; init; }
    }
}
