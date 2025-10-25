namespace LibraryManagmentSystem.Shared.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public static ApiResponse<T> Fail(string message) =>
            new ApiResponse<T> { Success = false, Message = message };
        public static ApiResponse<T> Fail(string message, List<string> errors) =>
            new ApiResponse<T> { Success = false, Message = message, Errors = errors };

        public static ApiResponse<T> Ok(T data, string? message = null) =>
        new ApiResponse<T> { Success = true, Data = data, Message = message };

    }
}
