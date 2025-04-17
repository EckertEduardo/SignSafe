using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace SignSafe.Presentation.ActionFilters.GlobalApi
{
    internal sealed class GlobalApiReturnFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                objectResult.StatusCode ??= (int)HttpStatusCode.InternalServerError;

                var statusCode = Enum.IsDefined(typeof(HttpStatusCode), objectResult.StatusCode)
                    ? (HttpStatusCode)objectResult.StatusCode
                    : HttpStatusCode.InternalServerError;

                objectResult.Value = new GlobalApiReturn
                {
                    Title = statusCode.ToString(),
                    Status = statusCode,
                    Type = objectResult.GetType().Name,
                    Data = objectResult.Value
                };
            }
        }
    }
}
