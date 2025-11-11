using LibraryManagmentSystem.Application.Feature.Documents.Commands.UploadDocument;
using LibraryManagmentSystem.Application.Feature.Documents.Queries.GetDocument;
using LibraryManagmentSystem.Application.Feature.Documents.Queries.PreviewDocument;
using LibraryManagmentSystem.Application.IClients;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Infrastructure.Data.MongoContext;
using LibraryManagmentSystem.Infrastructure.Data.Specifications.BooksSpecifications;
using LibraryManagmentSystem.Shared.Response;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class DocumentServices(IUnitOfWork unitOfWork, MongoDb mongoDB, ISupabaseClient supabase) : IDocumentServices
    {
        private readonly IGenericRepository<Book, Guid> bookRepository = unitOfWork.GetRepository<Book, Guid>();
        private readonly IMongoRepository<Document> documentRepository = unitOfWork.GetMongoRepository<Document>();
        List<string> allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".pdf" };

        public async Task<ApiResponse<string>> UploadDocumentAsync(UploadDocumentCommand command)
        {
            var book = await bookRepository.GetByIdAsync(command.bookId);
            if (book == null)
            {
                return ApiResponse<string>.Fail("Book not found", (int)HttpStatusCode.NotFound);
            }

            if (book.AuthorId != command.AuthorId)
            {
                return ApiResponse<string>.Fail("You are not authorized to upload a document for this book", (int)HttpStatusCode.Forbidden);
            }

            var fileExtension = Path.GetExtension(command.file.FileName).ToLowerInvariant();
            var fileName = $"{Guid.NewGuid()}_{command.file.FileName}";
            // رفع الملف على Supabase
            using var stream = command.file.OpenReadStream();
            var publicUrl = await supabase.UploadFileAsync(command.folderName, fileName, stream);




            var documentId = await CreateDocument(command.file, fileExtension, fileName, publicUrl);
            await UpdateBook(command.bookId, command.folderName, documentId, publicUrl);
            return ApiResponse<string>.Ok(fileName, $"Document {command.file.Name} Uploaded Successfuly");


        }
        public async Task<ApiResponse<string>> GetDocumentById(GetDocumentQuery query)
        {
            var docGuid = Guid.Parse(query.DocumentId);
            var document = await documentRepository.GetByIdAsync(docGuid);
            if (document == null)
            {
                return ApiResponse<string>.Fail("Document Not Found",(int) HttpStatusCode.NotFound);
            }
            return ApiResponse<string>.Ok(document.FilePath, "Document Retrieved Successfully");
        }
        public Task<ApiResponse<string>> DeleteDocumentAsync(string fileUrl, string folderName)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<string>> PreviewDocument(PreviewDocumentQuery documentQuery)
        {
            var spec = new BookSpecifications(documentQuery.DocumentId);
            var book = await bookRepository.GetByIdAsync(spec);
            if (book == null)
            {
                return ApiResponse<string>.Fail("Book Not Found", (int)HttpStatusCode.NotFound);
            }
            Guid? docId;
            string extension = "";
            if (documentQuery.Type == "cover")
            {
                docId = book.CoverImageId!;


            }
            else
            {
                docId = book.PdfId!;
                extension = "application/pdf";
            }
            if (docId == null)
            {
                return ApiResponse<string>.Fail("Document Not Found", (int)HttpStatusCode.NotFound);
            }
            var document = await documentRepository.GetByIdAsync(docId.Value);
            if (document == null)
            {
                return ApiResponse<string>.Fail("Document Not Found", (int)HttpStatusCode.NotFound);
            }
            if (documentQuery.Type == "cover")
            {
                extension = CheckExtension(document, extension);
            }
            var filePath = document.FilePath;
            if (!File.Exists(filePath))
            {
                return ApiResponse<string>.Fail("File Not Found", (int)HttpStatusCode.NotFound);
            }
            var fileBytes = await File.ReadAllBytesAsync(filePath);
            var base64String = Convert.ToBase64String(fileBytes);
            var result = $"data:{extension};base64,{base64String}";
            return ApiResponse<string>.Ok(result, "Document Preview Generated Successfully");
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
        private async Task UpdateBook(Guid bookId, string FolderName, Guid documentId, string documentUrl)
        {
            var book = await bookRepository.GetByIdAsync(bookId);
            if (FolderName == "images")
            {
                book.CoverImageId = documentId;
                book.CoverImageUrl = documentUrl;
            }
            else
            {
                book.PdfId = documentId;
                book.PdfUrl = documentUrl;
            }

            bookRepository.Update(book);
            await unitOfWork.SaveChangesAsync();

        }
        private static string CheckExtension(Document? doc, string extension)
        {
            switch (doc.FileType)
            {
                case ".png":
                    extension = "image/png";
                    break;
                case ".jpg":
                    extension = "image/jpeg";
                    break;
                default:
                    break;


            }

            return extension;
        }


    }
}
