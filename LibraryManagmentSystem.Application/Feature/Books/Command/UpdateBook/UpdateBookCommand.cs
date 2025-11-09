using LibraryManagmentSystem.Shared.DataTransferModel.Books;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Books.Command.UpdateBook
{
    public class UpdateBookCommand : IRequest<ApiResponse<BookDto>>
    {

        public Guid Id { get; set; } = default!;
        public string? Title { get; set; } = default!;


        public bool? IsAvailableForSale { get; set; }
        public bool? IsAvailable { get; set; }
        public string? CategoryName { get; set; } = default!;
        public int? PublishedYear { get; set; }
        public int? BorrowDurationDays { get; set; } = 10;
        public long? Price { get; set; }


        public string? Description { get; set; } = default!;
        public string? Language { get; set; } = default!;


        public int? Pages { get; set; }

        public string? Status { get; set; } = "Available";
    }
}
