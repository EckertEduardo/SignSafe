namespace SignSafe.Domain.Contracts.Api
{
    public class PaginatedRequest
    {
        public Pagination Pagination { get; set; } = new Pagination();
    }
    public class Pagination
    {
        private int _page;
        private int _size = 10;

        public int Page { get => _page; set => SetPage(value); }
        public int Size { get => _size; set => SetSize(value); }

        private void SetPage(int value)
        {
            if (value >= 1)
            {
                _page = value - 1;
            }
            else
            {
                _page = 0;
            }
        }

        private void SetSize(int value)
        {
            if (ValidSize(value))
            {
                _size = value;
            }
            else
            {
                _size = 10;
            }

            static bool ValidSize(int value)
            {
                return value >= 1 && value <= 100;
            }
        }
    }
}
