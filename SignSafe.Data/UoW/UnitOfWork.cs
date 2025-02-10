using SignSafe.Data.Context;
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

        public IUserRepository UserRepository { get; }

        public UnitOfWork(MyContext context, IUserRepository userRepository)
        {
            _context = context;
            UserRepository = userRepository;
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
