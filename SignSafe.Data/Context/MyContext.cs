using Microsoft.EntityFrameworkCore;
using SignSafe.Data.EntityConfig;
using SignSafe.Domain.Entities;

namespace SignSafe.Data.Context
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
