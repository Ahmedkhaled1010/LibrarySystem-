using LibraryManagmentSystem.Shared.DataTransferModel.Books;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Books.Command.UpdateBook
{
    public class UpdateBookCommand : IRequest<ApiResponse<BookDto>>
    {

        public Guid Id { get; set; } = default!;
        public string? Title { get; set; } = default!;


        public int? CopiesAvailable { get; set; } = 1;

        public string? CategoryName { get; set; } = default!;
        public int? PublishedYear { get; set; }
        public int? BorrowDurationDays { get; set; } = 10;
        public long? Price { get; set; }


        public string? Description { get; set; } = default!;
        public int? CopiesForSaleAvailable { get; set; }
        public string? Language { get; set; } = default!;


        public int? Pages { get; set; }

        public string? Status { get; set; } = "Available";
    }
}
