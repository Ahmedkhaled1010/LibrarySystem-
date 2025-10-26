using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Categories.Command.DeleteCategory
{
    public record DeleteCategoryCommand(string Id) : IRequest<ApiResponse<string>>;


}
