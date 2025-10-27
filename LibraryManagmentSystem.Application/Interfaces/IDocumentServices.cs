using LibraryManagmentSystem.Application.Feature.Documents.Commands.UploadDocument;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IDocumentServices
    {
        Task<ApiResponse<string>> UploadDocumentAsync(UploadDocumentCommand command);
        Task<ApiResponse<string>> DeleteDocumentAsync(string fileUrl, string folderName);

        Task<ApiResponse<string>> PreviewDocument(int fileId, string type);
    }
}
