using Microsoft.EntityFrameworkCore;
using SignSafe.Domain.Entities;
using SignSafe.Infrastructure.EntityConfig;

namespace SignSafe.Infrastructure.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfig());

            builder.Entity<User>().HasQueryFilter(x => !x.Deleted);
        }
    }
}
