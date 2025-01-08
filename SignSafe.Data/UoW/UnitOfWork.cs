using SignSafe.Data.Context;
using SignSafe.Data.Repositories;
using SignSafe.Domain.RepositoryInterfaces;

namespace SignSafe.Data.UoW
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        Task Commit();
    }
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MyContext _context;

        public IUserRepository UserRepository { get; private set; }

        public UnitOfWork(MyContext context)
        {
            _context = context;
            UserRepository = new UserRepository(context);
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
