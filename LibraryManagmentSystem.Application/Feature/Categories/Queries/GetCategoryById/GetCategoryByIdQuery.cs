using LibraryManagmentSystem.Shared.DataTransferModel.Category;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(string Id) : IRequest<ApiResponse<CategoryDto>>;


}
