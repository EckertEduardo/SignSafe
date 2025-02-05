﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SignSafe.Presentation.ApiFilters
{
    public class NotFoundActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult && objectResult.Value == null)
            {
                context.Result = new NotFoundObjectResult(new { message = "Not found" });
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
