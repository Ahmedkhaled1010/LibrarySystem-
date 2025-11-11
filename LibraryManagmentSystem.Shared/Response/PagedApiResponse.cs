using System.Net;

namespace LibraryManagmentSystem.Shared.Response
{
    public class PagedApiResponse<T> : ApiResponse<IReadOnlyList<T>>
    {
        public PaginationInfo Pagination { get; set; } = default!;
        public static PagedApiResponse<T> Ok(IReadOnlyList<T> data, PaginationInfo pagination, string message, int statusCode = (int)HttpStatusCode.OK)
        {
            return new PagedApiResponse<T>
            {
                Success = true,
                Data = data,
                Pagination = pagination,

                Message = message,
                StatusCode= statusCode

            };
        }
        public static PagedApiResponse<T> Fail(string message, int statusCode = (int)HttpStatusCode.InternalServerError)
        {
            return new PagedApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = Array.Empty<T>(),
                Pagination = new PaginationInfo(),
                StatusCode = statusCode
              
            };
        }
        public static PagedApiResponse<T> Fail(string message, List<string> errors, int statusCode = (int)HttpStatusCode.InternalServerError)
        {
            return new PagedApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors,
                Data = Array.Empty<T>(),
                Pagination = new PaginationInfo(),
                StatusCode=statusCode
            };
        }
    }
}
