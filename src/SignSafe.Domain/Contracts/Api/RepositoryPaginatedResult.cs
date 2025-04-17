namespace SignSafe.Domain.Contracts.Api
{
    public class RepositoryPaginatedResult<TData>
    {
        public RepositoryPaginatedResult(List<TData> data, int totalItems)
        {
            Data = data;
            TotalItems = totalItems;
        }

        public List<TData> Data { get; private set; }
        public int TotalItems { get; private set; }
    }
}
