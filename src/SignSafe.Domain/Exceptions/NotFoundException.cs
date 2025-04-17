namespace SignSafe.Domain.Exceptions
{
    public class NotFoundException(string parameter, object value) : Exception($"({parameter}: {value}) was not found")
    {
    }
}
