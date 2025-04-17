namespace SignSafe.Domain.Filters
{
    public record UserFilters
    {
        public string? Name { get; init; }
        public string? Email { get; init; }
    }
}
