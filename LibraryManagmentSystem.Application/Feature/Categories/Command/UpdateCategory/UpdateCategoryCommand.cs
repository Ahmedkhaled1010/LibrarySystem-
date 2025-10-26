using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Categories.Command.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<ApiResponse<string>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public UpdateCategoryCommand(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
