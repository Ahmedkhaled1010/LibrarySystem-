using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Books.Command.DeleteBook
{
    public record DeleteBookCommand(Guid Id) : IRequest<ApiResponse<string>>;

}
