using System.Net;

namespace LibraryManagmentSystem.Shared.Error
{
    public class ValidationErrorModel
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;
        public string Message { get; set; } = "Validation Failed";
        public IEnumerable<ValidationError> Errors { get; set; } = [];
    }
}
