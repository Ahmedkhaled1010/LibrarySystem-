using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Documents.Commands.UploadDocument
{
    public class UploadDocumentCommandValidator : AbstractValidator<UploadDocumentCommand>
    {

        List<string> allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".pdf" };

        public UploadDocumentCommandValidator()
        {
            RuleFor(x => x.file).NotNull().WithMessage("File is required.")
                .Must(file =>
                {
                    var extension = Path.GetExtension(file.FileName).ToLower();
                    return allowedExtensions.Contains(extension);
                })
                .WithMessage($"Invalid file type. Allowed types are: {string.Join(", ", allowedExtensions)}");
            RuleFor(x => x.folderName).NotEmpty().WithMessage("Folder name is required.");
            RuleFor(x => x.bookId).NotEmpty().WithMessage("Book ID is required.");
            RuleFor(x => x.file).Must(file => file.Length > 0).WithMessage("File cannot be empty.");

        }
    }
}
