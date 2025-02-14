using MediatR;
using SignSafe.Application.Users.Dtos;
using System.ComponentModel.DataAnnotations;

namespace SignSafe.Application.Users.Queries.Get
{
    public class GetUserQuery : IRequest<UserDto>
    {
        [Required]
        public long UserId { get; set; }
    }
}
