using LibraryManagmentSystem.Shared.DataTransferModel.Books;
using LibraryManagmentSystem.Shared.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace LibraryManagmentSystem.Application.Feature.Books.Command.CreateBook
{
    public class CreateBookCommand : IRequest<ApiResponse<BookDto>>
    {


        public string Title { get; set; } = default!;


        public string Description { get; set; } = default!;
        public string Language { get; set; } = default!;

        public string CategoryName { get; set; } = default!;
        public int PublishedYear { get; set; }
        public int BorrowDurationDays { get; set; } = 10;
        public long Price { get; set; }
        public int Pages { get; set; }

        [JsonIgnore]
        public string AuthorId { get; set; } = "";
        public string Status { get; set; } = "Available";



    }
}
