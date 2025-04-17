namespace SignSafe.Application.Auth
{
    public sealed class UserTokenInfo
    {
        public long UserId { get; init; }
        public string Email { get; init; }
        public string[] Roles { get; init; }

        public UserTokenInfo(long userId, string email, string[] roles)
        {
            UserId = userId;
            Email = email;
            Roles = roles;
        }
    }
}
