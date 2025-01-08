using MediatR;
using SignSafe.Domain.Contracts.Api;
using SignSafe.Domain.Dtos.Users;

namespace SignSafe.Application.Users.Queries.GetAll
{
    public class GetUsersByFilterQuery : PaginatedRequest, IRequest<PaginatedResult<List<UserDto>>>
    {
        public string? Name { get; set; }
        public string? Email { get; set; }

    }
}
