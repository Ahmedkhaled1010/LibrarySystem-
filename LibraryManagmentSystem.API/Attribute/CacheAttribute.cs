using LibraryManagmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace LibraryManagmentSystem.API.Attribute
{
    class CacheAttribute(int DurationInSecond = 20) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string CacheKey = CreateCacheKey(context.HttpContext.Request);
            ICasheServices cacheServices = context.HttpContext.RequestServices.GetRequiredService<ICasheServices>();
            var CacheValue = await cacheServices.GetAsync(CacheKey);
            if (CacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = CacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                };
                return;
            }

            var ExcecutedContext = await next.Invoke();

            if (ExcecutedContext.Result is OkObjectResult result)
            {
                await cacheServices.SetAsync(CacheKey, result.Value, TimeSpan.FromMinutes(DurationInSecond));

            }
        }
        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder key = new StringBuilder();
            key.Append(request.Path + '?');
            foreach (var item in request.Query.OrderBy(q => q.Key))
            {
                key.Append($"{item.Key}={item.Value}$");
            }
            return key.ToString();
        }
    }
}
