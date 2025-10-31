using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Documents.Queries.GetDocument
{
    public class GetDocumentQuery : IRequest<ApiResponse<string>>
    {
        public string DocumentId { get; set; } = default!;
    }
}
