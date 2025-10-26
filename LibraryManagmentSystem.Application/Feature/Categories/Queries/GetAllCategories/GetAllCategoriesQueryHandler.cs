using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.Category;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler(IServicesManager servicesManager) : IRequestHandler<GetAllCategoriesQuery, PagedApiResponse<CategoryDto>>
    {
        public Task<PagedApiResponse<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {

            var pagedCategories = servicesManager.CategoryServices.GetAllCategories(request);
            return pagedCategories;
        }
    }
}
