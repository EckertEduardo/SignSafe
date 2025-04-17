using Microsoft.EntityFrameworkCore;
using SignSafe.Domain.Contracts.Api;
using SignSafe.Domain.Entities;
using SignSafe.Domain.Extensions;
using SignSafe.Domain.Filters;
using SignSafe.Domain.RepositoryInterfaces;
using SignSafe.Infrastructure.Context;

namespace SignSafe.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MyContext context) : base(context)
        {
        }

        public async Task<RepositoryPaginatedResult<User>> GetByFilter(UserFilters filter, Pagination pagination)
        {
            var query = _context.Set<User>().AsNoTracking()
                .WhereIf(!string.IsNullOrEmpty(filter.Name), x => x.Name == filter.Name)
                .WhereIf(!string.IsNullOrEmpty(filter.Email), x => x.Email == filter.Email)
                .Select(x => x);

            var count = await query.CountAsync();

            var result = await query
                .Skip(pagination.Page)
                .Take(pagination.Size)
                .ToListAsync();

            return new RepositoryPaginatedResult<User>(result, count);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Set<User>().AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
