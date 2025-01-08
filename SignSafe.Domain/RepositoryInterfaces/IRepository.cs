using SignSafe.Domain.Entities;

namespace SignSafe.Domain.RepositoryInterfaces
{
    public interface IRepository<TEntity> where TEntity : Base
    {
        Task<TEntity> Get(long id);
        Task Insert(TEntity entity);
        void Update(TEntity entity);
        Task Delete(long id);
    }
}
