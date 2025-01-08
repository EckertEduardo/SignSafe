namespace SignSafe.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            return Enum.GetName(type, value) ?? string.Empty;
        }
    }
}
