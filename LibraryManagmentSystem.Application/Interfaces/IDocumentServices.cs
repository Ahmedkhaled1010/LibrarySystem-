using LibraryManagmentSystem.Application.Feature.Documents.Commands.UploadDocument;
using LibraryManagmentSystem.Application.Feature.Documents.Queries.GetDocument;
using LibraryManagmentSystem.Application.Feature.Documents.Queries.PreviewDocument;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IDocumentServices
    {
        Task<ApiResponse<string>> UploadDocumentAsync(UploadDocumentCommand command);
        Task<ApiResponse<string>> DeleteDocumentAsync(string fileUrl, string folderName);
        Task<ApiResponse<string>> GetDocumentById(GetDocumentQuery documentId);
        Task<ApiResponse<string>> PreviewDocument(PreviewDocumentQuery get);
    }
}
