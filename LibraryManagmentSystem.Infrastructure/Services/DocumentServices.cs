using LibraryManagmentSystem.Application.Feature.Documents.Commands.UploadDocument;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Infrastructure.Data.MongoContext;
using LibraryManagmentSystem.Shared.Response;
using Microsoft.AspNetCore.Http;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class DocumentServices(IUnitOfWork unitOfWork, MongoDb mongoDB) : IDocumentServices
    {
        private readonly IGenericRepository<Book, Guid> bookRepository = unitOfWork.GetRepository<Book, Guid>();
        private readonly IMongoRepository<Document> documentRepository = unitOfWork.GetMongoRepository<Document>();
        List<string> allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".pdf" };

        public async Task<ApiResponse<string>> UploadDocumentAsync(UploadDocumentCommand command)
        {
            using var session = await mongoDB.Database.Client.StartSessionAsync();
            session.StartTransaction();
            try
            {
                var book = await bookRepository.GetByIdAsync(command.bookId);
                if (book == null || book.AuthorId != command.AuthorId)
                {
                    return ApiResponse<string>.Fail("Book not found or you are not authorized to upload document for this book");
                }
                var fileExtension = Path.GetExtension(command.file.FileName).ToLowerInvariant();

                var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", command.folderName);
                var fileName = $"{Guid.NewGuid()}_{command.file.FileName}";
                var filePath = Path.Combine(FolderPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    command.file.CopyTo(stream);
                }
                var documentId = await CreateDocument(command.file, fileExtension, fileName, filePath);
                await UpdateBook(command.bookId, command.folderName, documentId);
                await session.CommitTransactionAsync();
                return ApiResponse<string>.Ok(fileName, $"Document {command.file.Name} Uploaded Successfuly");
            }
            catch (Exception ex)
            {
                await session.AbortTransactionAsync();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", command.folderName, command.file.FileName);
                if (File.Exists(filePath))
                    File.Delete(filePath);

                return ApiResponse<string>.Fail($"Upload failed: {ex.Message}");
            }

        }
        public Task<ApiResponse<string>> DeleteDocumentAsync(string fileUrl, string folderName)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> PreviewDocument(int fileId, string type)
        {
            throw new NotImplementedException();
        }
        private async Task<Guid> CreateDocument(IFormFile file, string extension, string fileName, string filePath)
        {
            Document document = new Document()
            {
                FileName = fileName,
                FilePath = filePath,
                FileSize = file.Length,
                FileType = extension
            };
            await documentRepository.AddAsync(document);
            await unitOfWork.SaveChangesAsync();
            return document.Id;
        }
        private async Task UpdateBook(Guid bookId, string FolderName, Guid documentId)
        {
            var book = await bookRepository.GetByIdAsync(bookId);
            if (FolderName == "Images")
            {
                book.CoverImageId = documentId;
            }
            else
            {
                book.PdfId = documentId;
            }

            bookRepository.Update(book);
            await unitOfWork.SaveChangesAsync();

        }



    }
}
