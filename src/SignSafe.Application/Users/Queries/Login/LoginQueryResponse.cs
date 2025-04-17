namespace SignSafe.Application.Users.Queries.Login
{
    public record LoginQueryResponse
    {
        public string JwtToken { get; init; } = string.Empty;
        public DateTime ExpiresIn { get; init; }
    }
}
