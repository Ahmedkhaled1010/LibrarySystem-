using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Documents.Queries.PreviewDocument
{
    public class PreviewDocumentQuery : IRequest<ApiResponse<string>>
    {
        public string DocumentId { get; set; } = default!;
        public string Type { get; set; }
    }
}
