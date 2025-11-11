using System.Net;

namespace LibraryManagmentSystem.Shared.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; }

        public List<string>? Errors { get; set; }

        public static ApiResponse<T> Fail(string message , int statusCode = (int)HttpStatusCode.InternalServerError) =>
            new ApiResponse<T> { Success = false, Message = message ,StatusCode=statusCode};
        public static ApiResponse<T> Fail(string message, List<string> errors, int statusCode = (int)HttpStatusCode.InternalServerError) =>
            new ApiResponse<T> { Success = false, Message = message, Errors = errors,StatusCode=statusCode };

        public static ApiResponse<T> Ok(T data, string? message = null, int statusCode = (int)HttpStatusCode.OK) =>
        new ApiResponse<T> { Success = true, Data = data, Message = message, StatusCode =statusCode };

    }
}
