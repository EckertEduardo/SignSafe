using MediatR;
using SignSafe.Application.Users.Dtos;
using SignSafe.Domain.Contracts.Api;

namespace SignSafe.Application.Users.Queries.GetAll
{
    public class GetUsersByFilterQuery : PaginatedRequest, IRequest<PaginatedResult<List<UserDto>>>
    {
        public string? Name { get; set; }
        public string? Email { get; set; }

    }
}
