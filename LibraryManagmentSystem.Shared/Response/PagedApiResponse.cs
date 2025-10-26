namespace LibraryManagmentSystem.Shared.Response
{
    public class PagedApiResponse<T> : ApiResponse<IReadOnlyList<T>>
    {
        public PaginationInfo Pagination { get; set; } = default!;
        public static PagedApiResponse<T> Ok(IReadOnlyList<T> data, PaginationInfo pagination, string message)
        {
            return new PagedApiResponse<T>
            {
                Success = true,
                Data = data,
                Pagination = pagination,

                Message = message,

            };
        }
        public static PagedApiResponse<T> Fail(string message)
        {
            return new PagedApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = Array.Empty<T>(),
                Pagination = new PaginationInfo()
            };
        }
        public static PagedApiResponse<T> Fail(string message, List<string> errors)
        {
            return new PagedApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors,
                Data = Array.Empty<T>(),
                Pagination = new PaginationInfo()
            };
        }
    }
}
