using LibraryManagmentSystem.Shared.Error;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.API.Extensions
{
    public class ApiResponseHandle
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext context)
        {
            var errors = context.ModelState
                   .Where(e => e.Value.Errors.Count > 0)
                   .Select(e => new ValidationError()
                   {
                       Field = e.Key,
                       Errors = e.Value.Errors.Select(e => e.ErrorMessage)
                   }).ToArray();
            var toReturn = new ValidationErrorModel()
            {

                Errors = errors
            };
            return new BadRequestObjectResult(toReturn);
        }
    }
}
