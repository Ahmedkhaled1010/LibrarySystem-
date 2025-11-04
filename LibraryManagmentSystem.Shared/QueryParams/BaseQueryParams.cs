namespace LibraryManagmentSystem.Shared.QueryParams
{
    public class BaseQueryParams
    {
        private const int DefaultPageSize = 12;
        private const int MaxPageSize = 20;
        public int pageSize = DefaultPageSize;
        public int pageNumber { get; set; } = 1;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
    }
}

