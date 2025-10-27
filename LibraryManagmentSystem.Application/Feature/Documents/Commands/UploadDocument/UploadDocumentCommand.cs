using LibraryManagmentSystem.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace LibraryManagmentSystem.Application.Feature.Documents.Commands.UploadDocument
{
    public class UploadDocumentCommand : IRequest<ApiResponse<string>>
    {
        public IFormFile file { get; set; }

        public string folderName { get; set; }
        public Guid bookId { get; set; }
        [JsonIgnore]
        public string AuthorId { get; set; } = "";
    }
}
