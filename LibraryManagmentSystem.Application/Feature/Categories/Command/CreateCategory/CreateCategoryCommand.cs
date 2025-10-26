using LibraryManagmentSystem.Shared.DataTransferModel.Category;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Categories.Command.CreateCategory
{
    public class CreateCategoryCommand : IRequest<ApiResponse<CategoryDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }


        public CreateCategoryCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
