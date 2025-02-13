using MediatR;
using SignSafe.Domain.Dtos.Users;
using System.ComponentModel.DataAnnotations;

namespace SignSafe.Application.Users.Commands.Insert
{
    public class InsertUserCommand : IRequest
    {
        [Required]
        public required InsertUserDto InsertUserDto { get; set; }
    }
}
