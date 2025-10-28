using LibraryManagmentSystem.Shared.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace LibraryManagmentSystem.Application.Feature.Borrow.Command.ReturnBook
{
    public class ReturnBookCommand : IRequest<ApiResponse<string>>
    {
        public Guid BookId { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}
