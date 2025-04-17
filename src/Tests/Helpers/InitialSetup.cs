using Microsoft.Extensions.Configuration;

namespace Tests.Helpers
{
    public static class InitialSetup
    {
        public static IConfiguration SetupConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }
    }
}
