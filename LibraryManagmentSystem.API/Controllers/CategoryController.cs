using LibraryManagmentSystem.API.Attribute;
using LibraryManagmentSystem.Application.Feature.Categories.Command.CreateCategory;
using LibraryManagmentSystem.Application.Feature.Categories.Command.DeleteCategory;
using LibraryManagmentSystem.Application.Feature.Categories.Command.UpdateCategory;
using LibraryManagmentSystem.Application.Feature.Categories.Queries.GetAllCategories;
using LibraryManagmentSystem.Application.Feature.Categories.Queries.GetCategoryById;
using LibraryManagmentSystem.Shared.QueryParams;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Cache]
        public async Task<IActionResult> GetAllCategories([FromQuery] CategoryQueryParams categoryQueryParams)
        {
            var result = await mediator.Send(new GetAllCategoriesQuery(categoryQueryParams));
            return Ok(result);
        }
        [HttpGet("{id}")]
        [Cache]

        public async Task<IActionResult> GetCategoryById(string id)
        {
            var result = await mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand categoryCommand)
        {
            var result = await mediator.Send(categoryCommand);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand categoryCommand)
        {
            var result = await mediator.Send(categoryCommand);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var result = await mediator.Send(new DeleteCategoryCommand(id));
            return Ok(result);
        }
    }
}
