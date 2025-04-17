using SignSafe.Domain.Contracts.Api;
using SignSafe.Domain.Entities;
using SignSafe.Domain.Filters;

namespace SignSafe.Domain.RepositoryInterfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<RepositoryPaginatedResult<User>> GetByFilter(UserFilters filter, Pagination pagination);
        Task<User?> GetByEmail(string email);
    }
}
