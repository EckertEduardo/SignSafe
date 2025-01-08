using Microsoft.EntityFrameworkCore;
using SignSafe.Data.Context;
using SignSafe.Domain.Entities;
using SignSafe.Domain.RepositoryInterfaces;

namespace SignSafe.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Base
    {
        internal readonly MyContext _context;
        internal DbSet<TEntity> _dbSet;
        public Repository(MyContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async virtual Task<TEntity> Get(long id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async virtual Task Insert(TEntity entity)
        {
            entity.SetCreatedAt();

            await _dbSet.AddAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            entity.SetUpdatedAt();

            _dbSet.Update(entity);
        }

        public async Task Delete(long id)
        {
            var entity = await Get(id);
            if (entity is null)
                return;

            entity.SetDeletedAt();

            _dbSet.Update(entity);
        }
    }
}
