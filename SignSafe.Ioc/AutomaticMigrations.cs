using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SignSafe.Infrastructure.Context;

namespace SignSafe.Ioc
{
    public static class AutomaticMigrations
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope
                        .ServiceProvider
                        .GetRequiredService<MyContext>();

                if (context.Database.IsRelational())
                {
                    context
                        .Database
                        .Migrate();
                }
            };
        }
    }
}
