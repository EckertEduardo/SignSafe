namespace SignSafe.Domain.Contracts.Api
{
    public abstract class PaginatedResult
    {
        public int Page { get; protected set; }
        public int TotalPages { get; protected set; }
        public int TotalItems { get; protected set; }
    }

    public class PaginatedResult<TData> : PaginatedResult where TData : class
    {
        public PaginatedResult(TData data, int page, int sizePage, int totalItems)
        {
            var totalPages = Math.Ceiling((decimal)totalItems / sizePage);

            Data = data;
            Page = page + 1;
            TotalPages = (int)totalPages;
            TotalItems = totalItems;
        }
        public TData Data { get; private set; }
    }
}
