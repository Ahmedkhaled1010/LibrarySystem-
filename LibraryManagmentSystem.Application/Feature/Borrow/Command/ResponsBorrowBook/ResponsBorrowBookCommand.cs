using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Borrow.Command.ResponsBorrowBook
{
    public class ResponsBorrowBookCommand : IRequest<ApiResponse<string>>
    {
        public string UserId { get; set; } = default!;
        public string BookId { get; set; } = default!;
        public string BookTitle { get; set; } = default!;
        public string RequestId { get; set; } = default!;
        public bool IsApproved { get; set; }
    }
}
