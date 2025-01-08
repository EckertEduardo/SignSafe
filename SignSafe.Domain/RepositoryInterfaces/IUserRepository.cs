using SignSafe.Domain.Contracts.Api;
using SignSafe.Domain.Dtos.Users;
using SignSafe.Domain.Entities;

namespace SignSafe.Domain.RepositoryInterfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<RepositoryPaginatedResult<User>> GetByFilter(UsersFilterDto filter, Pagination pagination);
        Task<User?> GetByEmail(string email);
    }
}
