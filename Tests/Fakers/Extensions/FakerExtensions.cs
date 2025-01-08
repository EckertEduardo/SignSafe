using Bogus;

namespace Tests.Fakers.Extensions
{
    public static class FakerExtensions
    {
        public static long GetId()
        {
            return new Faker().Random.Number(1, 99999);
        }
    }
}