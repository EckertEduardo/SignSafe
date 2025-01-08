using Microsoft.EntityFrameworkCore;
using SignSafe.Data.Context;

namespace SignSafe.Ioc
{
    public static class AutomaticMigrations
    {
        public static void Run(MyContext context)
        {
            if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                context
                    .Database
                    .Migrate();
            }
        }
    }
}
