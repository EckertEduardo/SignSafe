namespace SignSafe.Domain.Entities.Validations
{
    public static class UserValidations
    {
        public const int PASSWORD_MIN_LENGTH = 8;
        public const int PHONE_NUMBER_MIN_LENGTH = 8;
        public const int PHONE_NUMBER_MAX_LENGTH = 20;
        public static readonly DateTime BIRTH_DATE_MIN = new(year: 1900, month: 01, day: 01);
        public static readonly DateTime BIRTH_DATE_MAX = DateTime.UtcNow;
    }
}
