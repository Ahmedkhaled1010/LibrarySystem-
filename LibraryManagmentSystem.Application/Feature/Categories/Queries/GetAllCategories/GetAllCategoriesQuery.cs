using LibraryManagmentSystem.Shared.DataTransferModel.Category;
using LibraryManagmentSystem.Shared.QueryParams;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Categories.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery(CategoryQueryParams CategoryQueryParams) : IRequest<PagedApiResponse<CategoryDto>>;

}
