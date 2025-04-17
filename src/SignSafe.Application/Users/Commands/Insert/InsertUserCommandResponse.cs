namespace SignSafe.Application.Users.Commands.Insert
{
    public record InsertUserCommandResponse
    {
        public long UserId { get; init; }

        public InsertUserCommandResponse(long userId)
        {
            UserId = userId;
        }
    }
}
